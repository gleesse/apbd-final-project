using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockPortfolio.Models;
using StockPortfolio.Server.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        public UserRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User_Stock> AddToWatchlistAsync(int userID, string stockSymbol)
        {
            var user = await _dbContext.Set<User>().Include(u => u.User_Stocks).FirstOrDefaultAsync(u => u.UserID == userID);
            if (user is null) return null;
            var isAlreadyAdded = user.User_Stocks.Any(us => us.StockReference?.Symbol == stockSymbol);
            if (isAlreadyAdded) return null;
            var stock = await _dbContext.Set<Stock>().FirstOrDefaultAsync(s => s.Symbol == stockSymbol);
            if (stock is null) return null;
            var result = await _dbContext.Set<User_Stock>().AddAsync(new User_Stock()
            {
                StockReference = stock,
                UserReference = user
            });
            return result.Entity;
        }

        public async Task<IEnumerable<Stock>> GetAllFollowedStocksAsync(int userID)
        {
            var user = await _dbContext.Set<User>().Include(u => u.User_Stocks).FirstOrDefaultAsync(u => u.UserID == userID);
            if (user is null || user.User_Stocks is null || user.User_Stocks.Count < 1) return null;
            var stocks = new List<Stock>();

            foreach(var stock in user.User_Stocks)
            {
                stocks.Add(stock.StockReference);
            }
            return stocks;
        }

        public async Task<User> RemoveFromWatchlistAsync(int userID, string stockSymbol)
        {
            var user = await _dbContext.Set<User>().Include(u => u.User_Stocks).FirstOrDefaultAsync(u => u.UserID == userID);
            if (user is null || user.User_Stocks is null || user.User_Stocks.Count < 1) return null;

            var user_stock = user.User_Stocks.FirstOrDefault(e => e.StockReference?.Symbol == stockSymbol);
            if (user_stock is null) return null;
            var updatedUser = user;
            updatedUser.User_Stocks.Remove(user_stock);
            _dbContext.Entry(user).CurrentValues.SetValues(updatedUser);
            return updatedUser;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #region Not Implemented

        public Task<IEnumerable<User_Stock>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<User_Stock> UpdateAsync(int id, User_Stock entity)
        {
            throw new NotImplementedException();
        }
        public Task<User_Stock> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<User_Stock> AddAsync(User_Stock entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public interface IUserRepository : IRepository<User_Stock>
    {
        public Task<User_Stock> AddToWatchlistAsync(int userID, string stockSymbol);
        public Task<IEnumerable<Stock>> GetAllFollowedStocksAsync(int userID);
        public Task<User> RemoveFromWatchlistAsync(int userID, string stockSymbol);
    }
}
