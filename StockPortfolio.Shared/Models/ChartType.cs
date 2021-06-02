using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Blazor;

namespace StockPortfolio.Models
{
    public class ChartType
    {
        public StockChartSeriesType Value { get; set; }
        public string Text { get { return Value.ToString(); } }

        public static implicit operator StockChartSeriesType(ChartType type) => type.Value;
        public static explicit operator ChartType(StockChartSeriesType type) => new() { Value = type };

        public static List<ChartType> GetAvailableChartTypes()
        {
            return new List<ChartType>()
                {
                    new ChartType { Value = StockChartSeriesType.Candlestick },
                    new ChartType { Value = StockChartSeriesType.OHLC }
                };
        }
    }
}
