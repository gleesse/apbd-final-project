using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Blazor;

namespace StockPortfolio.Models
{
    public class Interval
    {
        public ChartCategoryAxisBaseUnit Unit { get; set; }

        public int Step { get; set; }
    }
}
