using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Extensions
{
    public static class UserExtensions
    {
        public static byte[] RenewRefreshToken(this User user, double lifeTimeDays)
        {
            var refreshToken = Guid.NewGuid().ToByteArray();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = DateTime.Now.AddDays(lifeTimeDays);
            return refreshToken;
        }
    }
}
