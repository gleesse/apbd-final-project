using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Models
{
    public class StockRequestDTO
    {
        [Required]
        public string Symbol { get; set; }
        [Required]
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
    }
}
