using System.Collections.Generic;

namespace StockPortfolio.Models
{
    public class Stock : EntityBase
    {
        public string Symbol { get; set; }

        public int StockID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DayChange { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal VolumeAvg { get; set; }
        public decimal MarketCap { get; set; }
        public decimal? PricePerEarningRatio { get; set; }

        public ICollection<User_Stock> User_Stocks { get; set; }
    }
}
