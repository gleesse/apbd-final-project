using StockPortfolio.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Blazor;

namespace StockPortfolio.Models
{
    public class IntervalFilter
    {
        public string Name { get; set; }
        public Interval Interval { get; set; }
        public int GapInMinutes { get; set; }

        public static List<IntervalFilter> GetFilters()
        {
            return new List<IntervalFilter>()
                {
                    new IntervalFilter { Name = "5M",  Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Minutes, Step = 5 }, GapInMinutes=5 },
                    new IntervalFilter { Name = "15M", Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Minutes, Step = 15 }, GapInMinutes=15 },
                    new IntervalFilter { Name = "30M", Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Minutes, Step = 30 }, GapInMinutes=30 },
                    new IntervalFilter { Name = "1H",  Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Hours, Step = 1 }, GapInMinutes=60 },
                    new IntervalFilter { Name = "4H",  Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Hours, Step = 4 }, GapInMinutes=60 * 4 },
                    new IntervalFilter { Name = "1D",  Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Days, Step = 1 }, GapInMinutes=60 * 24 },
                    new IntervalFilter { Name = "1W",  Interval = new Interval { Unit = ChartCategoryAxisBaseUnit.Weeks, Step = 1 }, GapInMinutes=60 * 24 * 7 }
                };
        }
    }
}
