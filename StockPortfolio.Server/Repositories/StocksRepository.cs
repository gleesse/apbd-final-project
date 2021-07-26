using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockPortfolio.Models;
using StockPortfolio.Server.Contexts;
using StockPortfolio.Server.Extensions;
using StockPortfolio.Server.Services;

namespace StockPortfolio.Server.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IStocksInfoService _stocksInfoService;

        public StocksRepository(ApplicationContext dbContext, IStocksInfoService stocksInfoService)
        {
            _dbContext = dbContext;
            _stocksInfoService = stocksInfoService;
        }

        public async Task<Stock> AddAsync(Stock entity)
        {
            if (entity is null) return null;
            entity.LastUpdateDate = DateTime.Now;
            var createdEntityEntry = await _dbContext.Set<Stock>().AddAsync(entity);
            return createdEntityEntry.Entity;
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var stocksSet = _dbContext.Set<Stock>();
            var entityToRemove = await stocksSet.FindAsync(id);
            if (entityToRemove == null)
            {
                return null;
            }

            stocksSet.Remove(entityToRemove);
            return entityToRemove;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync() => await _dbContext.Set<Stock>().ToListAsync();

        public async Task<Stock> GetAsync(string symbol)
        {
            var actualStockInfo = await _stocksInfoService.GetStockAsync(symbol);
            var record = await _dbContext.Set<Stock>().FirstOrDefaultAsync(stock => stock.Symbol == symbol);

            if (actualStockInfo is null) return record;
            actualStockInfo.LastUpdateDate = DateTime.Now;
            if (record is null) await AddAsync(actualStockInfo);
            else
            {
                actualStockInfo.LastUpdateDate = DateTime.Now;
                _dbContext.Entry(record).CurrentValues.SetValues(actualStockInfo);
            }
            return actualStockInfo;
        }

        public async Task<Stock> UpdateAsync(int id, Stock entity)
        {
            if (id != entity.StockID)
            {
                return null;
            }
            var record = await _dbContext.Set<Stock>().FindAsync(id);
            if (record is null)
            {
                return null;
            }
            entity.LastUpdateDate = DateTime.Now;
            _dbContext.Entry(record).CurrentValues.SetValues(entity);
            return record;
        }

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        public async Task<StockIntervalDetails> GetIntervalDetailsAsync(string symbol)
        {
            return await _stocksInfoService.GetStockIntervalDetailsAsync(symbol);
        }

        public async Task<IEnumerable<SearchMatchStockModel>> GetBestMatchesAsync(string keyword)
        {
            return await _stocksInfoService.GetBestMatchesAsync(keyword);
        }
    }

    public interface IStocksRepository : IRepository<Stock>
    {
        public Task<Stock> GetAsync(string symbol);
        public Task<StockIntervalDetails> GetIntervalDetailsAsync(string symbol);
        public Task<IEnumerable<SearchMatchStockModel>> GetBestMatchesAsync(string keyword);
    }  
}

