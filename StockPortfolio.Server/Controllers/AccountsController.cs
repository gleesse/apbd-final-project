using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockPortfolio.Models;
using StockPortfolio.Server.Extensions;
using StockPortfolio.Server.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsersRepository _usersRepository;

        #region Configs

        private string AuthSecretKey => _config["Server:AuthSecretKey"];
        private double AccessTokenLifetime => double.Parse(_config["Server:AccessTokenLifetime"]);
        private double RefreshTokenLifetime => double.Parse(_config["Server:RefreshTokenLifetime"]);
        private int PasswordEncryptIterationsCount => int.Parse(_config["PasswordEncryptSpecifications:IterationsCount"]);
        private int PasswordEncryptKeyBytesCount => int.Parse(_config["PasswordEncryptSpecifications:KeyBytesCount"]);
        private int SaltLength => int.Parse(_config["PasswordEncryptSpecifications:SaltLength"]);
        #endregion

        public AccountsController(IConfiguration config, IUsersRepository usersRepository)
        {
            _config = config;
            _usersRepository = usersRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _usersRepository.GetAsync(loginRequest.Login);
            if (user is null) return NotFound();
            var givenPasswordEncrypted = loginRequest.Password.Encrypt(user.Credentials.Salt, PasswordEncryptIterationsCount, PasswordEncryptKeyBytesCount);
            if (!user.Credentials.PasswordHashed.SequenceEqual(givenPasswordEncrypted)) return StatusCode(401, "Wrong password");

            var creds = AuthSecretKey.ToSigningCredentials();
            var accessToken = GetNewJwtToken(creds, user);
            var refreshToken = user.RenewRefreshToken(RefreshTokenLifetime);

            var result = await _usersRepository.UpdateAsync(user.UserID, user);
            if (result is null) return StatusCode(500);
            await _usersRepository.SaveAsync();

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                refreshToken = refreshToken
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = await _usersRepository.GetAsync(registerRequest.Login);
            if (user != null) return StatusCode(409, "Such user already exists");
            user = CreateUser(registerRequest);

            await _usersRepository.AddAsync(user);
            await _usersRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshAccessTokenAsync([FromHeader] string login, [FromHeader] byte[] refreshToken)
        { 
            var user = await _usersRepository.GetAsync(login);
            if (user is null) return StatusCode(401, "No such user was found");
            if (!user.RefreshToken.SequenceEqual(refreshToken)) return StatusCode(401, "Wrong refresh token");
            if (user.RefreshTokenExpirationDate.Value.CompareTo(DateTime.Now) < 0) return StatusCode(401, "Refresh token has expired");

            var creds = AuthSecretKey.ToSigningCredentials();
            var accessToken = GetNewJwtToken(creds, user);

            return Ok(new { accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken) });
        }

        #region Utility

        private JwtSecurityToken GetNewJwtToken(SigningCredentials creds, User user)
        {
            var role = user.IsAdmin ? "Admin" : "User";
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Credentials.Login),
                new Claim(ClaimTypes.Role, role)
            };

            return new
            (
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(AccessTokenLifetime),
                signingCredentials: creds
            );
        }

        private User CreateUser(RegisterRequest registerRequest)
        {
            var salt = GetSalt();
            var userCredentials = new UserCredentials()
            {
                Login = registerRequest.Login,
                PasswordHashed = registerRequest.Password.Encrypt(salt, PasswordEncryptIterationsCount, PasswordEncryptKeyBytesCount),
                Salt = salt
            };
            return new User()
            {
                Credentials = userCredentials,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                RefreshToken = Guid.NewGuid().ToByteArray(),
                RefreshTokenExpirationDate = DateTime.Now.AddDays(RefreshTokenLifetime)
            };
        }

        private byte[] GetSalt()
        {
            var salt = new byte[SaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        #endregion
    }
}
