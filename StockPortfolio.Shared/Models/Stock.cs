using StockPortfolio.Models;
using System.Collections.Generic;

namespace StockPortfolio.Models
{
    public class Stock
    {
        public Stock()
        {
            User_Stocks = new HashSet<User_Stock>();
        }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal DayChange { get; set; }

        public decimal ChangePercentage { get; set; }

        public decimal Volume { get; set; }

        public decimal VolumeAvg { get; set; }

        public decimal MarketCap { get; set; }

        public decimal? PricePerEarningRatio { get; set; }

        public bool IsCategorized { get; set; }

        public ICollection<User_Stock> User_Stocks { get; set; }
    }
}
