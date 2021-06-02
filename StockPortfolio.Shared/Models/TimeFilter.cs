using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class TimeFilter
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public int DurationInHours { get; set; }

        public static List<TimeFilter> GetFilters()
        {
            return new List<TimeFilter>()
                {
                   new TimeFilter { Name = "1H", DurationInHours = 1 },
                   new TimeFilter { Name = "4H", DurationInHours = 4 },
                   new TimeFilter { Name = "12H",DurationInHours = 12 },
                   new TimeFilter { Name = "1D", DurationInHours = 24 },
                   new TimeFilter { Name = "4D", DurationInHours = 24 * 4 },
                   new TimeFilter { Name = "1W", DurationInHours = 24 * 7 }
                };
        }
    }
}
