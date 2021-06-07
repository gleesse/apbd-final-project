using StockPortfolio.Helpers;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPortfolio.Services
{
    internal class StocksRepository
    {
        private List<Stock> UserStocks { get; set; }
        public List<StockIntervalDetails> GenerateStockIntervals(Stock stock, int intervalInMinutes, DateTime start, DateTime end)
        {
            DateTime minStart = Constants.GetMinDate();
            DateTime maxEnd = Constants.GetMaxDate();
            if (start < minStart)
            {
                start = minStart;
            }
            if (end > maxEnd)
            {
                end = maxEnd;
            }

            var stocks = new List<StockIntervalDetails>
            {
                new StockIntervalDetails
                {
                    Open = stock.Price - 0.1m,
                    Close = stock.Price,
                    Date = start,
                    High = stock.Price + 1.1m,
                    Low = stock.Price - 2.2m,
                    Volume = stock.Price * 10000
                }
            };
            var startIterator = start.AddMinutes(intervalInMinutes);

            var random = new Random();

            var counter = 1;
            while (startIterator < end)
            {
                var prevInterval = stocks[counter - 1];
                // get random volatility from -3% to 3% for each N(intervalInMinutes) minutes 
                var randomVolatility = random.Next(-30, 30) * 0.001m;

                var randomVolume = random.Next(100, 10000);
                var randomHighPercentage = random.Next(101, 105) * 0.01m;
                var randomLowPercentage = random.Next(95, 99) * 0.01m;

                var change = prevInterval.Close * randomVolatility;
                var newPrice = prevInterval.Close + change;
                var high = newPrice * randomHighPercentage;
                var low = newPrice * randomLowPercentage;

                low = Lowest(low, newPrice, prevInterval.Close, high);
                high = Highest(low, newPrice, prevInterval.Close, high);

                var stockToAdd = new StockIntervalDetails
                {
                    Close = newPrice,
                    Date = startIterator,
                    Open = prevInterval.Close,
                    High = high,
                    Low = low,
                    Volume = newPrice * randomVolume
                };

                stocks.Add(stockToAdd);

                counter++;
                startIterator = startIterator.AddMinutes(intervalInMinutes);
            }

            return stocks;
        }
        private decimal Lowest(params decimal[] inputs)
        {
            return inputs.Min();
        }

        private decimal Highest(params decimal[] inputs)
        {
            return inputs.Max();
        }

        public async Task<Stock> AddStock(Stock stockToAdd)
        {
            var matchingItem = UserStocks.FirstOrDefault(s => s.Symbol == stockToAdd.Symbol);

            return await Task.FromResult(matchingItem);
        }

        public async Task<Stock> RemoveStock(Stock stockToRemove)
        {
            var matchingItem = UserStocks.FirstOrDefault(s => s.Symbol == stockToRemove.Symbol);

            return await Task.FromResult(matchingItem);
        }

        public Task<List<Stock>> GetStocks(bool isCategorized)
        {
            if (UserStocks == null)
            {
                UserStocks = GetInitialStocks();
            }

            var categorizedStocks = UserStocks.ToList();

            return Task.FromResult(categorizedStocks);
        }

        private List<Stock> GetInitialStocks()
        {
            var stockList = new List<Stock>();

            stockList.Add(new Stock
            {
                Symbol = "AAN",
                Name = "Aaron's, Inc.",
                Price = 76.61m,
                DayChange = -1.18m,
                ChangePercentage = -1.52m,
                VolumeAvg = 837114,
                MarketCap = 5174814208,
                PricePerEarningRatio = 25.94m
            });

            return stockList;
        }
    }
}
