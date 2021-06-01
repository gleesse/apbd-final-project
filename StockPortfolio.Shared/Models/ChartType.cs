using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Blazor;

namespace StockPortfolio.Models
{
    public class ChartType
    {
        public ChartSeriesType Value { get; set; }
        public string Text { get { return Value.ToString(); } }

        public static implicit operator ChartSeriesType(ChartType type) => type.Value;
        public static explicit operator ChartType(ChartSeriesType type) => new() { Value = type };

        public static List<ChartType> GetAvailableChartTypes()
        {
            return new List<ChartType>()
                {
                    new ChartType { Value = ChartSeriesType.Candlestick },
                    new ChartType { Value = ChartSeriesType.Area },
                    new ChartType { Value = ChartSeriesType.Line }
                };
        }
    }
}
