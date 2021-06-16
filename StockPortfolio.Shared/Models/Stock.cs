using System;
using System.Collections.Generic;

namespace StockPortfolio.Models
{
    public class Stock : EntityBase
    {
        public Stock()
        {
            User_Stocks = new HashSet<User_Stock>();
        }

        public int StockID { get; set; }
        public string Symbol { get; set; } //ticker Details
        public string Name { get; set; } //ticker Details
        public string Ceo { get; set; } //ticker Details
        public string Industry { get; set; } //ticker Details
        public string OfficialURL { get; set; } //ticker Details
        public string LogoImageURL { get; set; } //ticker Details
        public int FullTimeEmployees { get; set; } //ticker Details
        public decimal Price { get; set; }
        public decimal DayChange { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal VolumeAvg { get; set; }
        public decimal MarketCap { get; set; } //ticker Details
        public decimal? PricePerEarningRatio { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        

        public ICollection<User_Stock> User_Stocks { get; set; }
    }
}
