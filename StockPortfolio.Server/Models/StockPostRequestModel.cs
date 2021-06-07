using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Models
{
    public class StockPostRequestModel
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Ceo { get; set; }
        public string Industry { get; set; }
        public string OfficialURL { get; set; }
        public string LogoImageURL { get; set; }
        public int FullTimeEmployees { get; set; }
        public decimal Price { get; set; }
        public decimal DayChange { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal VolumeAvg { get; set; }
        public decimal MarketCap { get; set; }
        public decimal PricePerEarningRatio { get; set; }
        public int[] UsersOwnersIDs { get; set; }
    }
}
