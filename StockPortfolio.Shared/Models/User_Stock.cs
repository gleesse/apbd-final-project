using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class User_Stock
    {
        public int UserID { get; set; }
        public User UserReference { get; set; }
        public int StockID { get; set; }
        public Stock StockReference{ get; set; }
    }
}
