using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class StockIntervalDetails
    {
        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal Close { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Volume { get; set; }

        public string ColumnColor { get; set; } // return currentLargerThenPrev ? '#5CB85C' : '#FF6358';
    }
}
