using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Services
{
    public interface IStocksInfoService
    {
        public Task<Stock> GetStockAsync(string symbol);
        public Task<StockIntervalDetails> GetStockIntervalDetailsAsync(string symbol);
        public Task<IEnumerable<SearchMatchStockModel>> GetBestMatchesAsync(string keyword);
    }
}
