using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Server.Repositories;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Controllers
{
    [Authorize]
    [Route("api/user/watchlist")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/add")]
        public async Task<ActionResult> AddStockToWatchlist([FromQuery] string symbol)
        {
            var result = await _userRepository.AddToWatchlistAsync(GetCurrentUserID(), symbol);
            if (result is null) return BadRequest();
            await _userRepository.SaveAsync();
            return Ok(result);
        }

        [HttpDelete("/remove")]
        public async Task<ActionResult> RemoveStockFromWatchlist([FromQuery] string symbol)
        {
            var result = await _userRepository.RemoveFromWatchlistAsync(GetCurrentUserID(), symbol);
            if (result is null) return BadRequest();
            await _userRepository.SaveAsync();
            return Ok(result);
        }

        [HttpGet("/all")]
        public async Task<ActionResult> GetAllFollowedStocks()
        {
            var result = await _userRepository.GetAllFollowedStocksAsync(GetCurrentUserID());
            return Ok(result);
        }

        private int GetCurrentUserID()
        {
            return int.Parse(User.FindFirst("UserID").Value);
        }
    }
}
