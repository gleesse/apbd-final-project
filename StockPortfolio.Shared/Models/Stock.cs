using Newtonsoft.Json;
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
        public decimal MarketCap { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        

        public ICollection<User_Stock> User_Stocks { get; set; }
    }
}
