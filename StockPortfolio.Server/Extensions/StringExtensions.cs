using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Extensions
{
    public static class StringExtensions
    {
        public static byte[] Encrypt(this string @string, byte[] salt, int iterationsCount, int keyBytesCount)
        {
            var encryptedString = new Rfc2898DeriveBytes(@string, salt, iterationsCount);
            return encryptedString.GetBytes(keyBytesCount);
        }

        public static SigningCredentials ToSigningCredentials(this string @string)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(@string));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
    }
}
