using System;
using System.Collections.Generic;
using System.Linq;
using StockPortfolio.Models;
using StockPortfolio.Services;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor;

namespace StockPortfolio.Components
{
    public partial class StockDetailsVisualizer : ComponentBase
    {
        [Inject] private StocksRepository StocksRepo { get; set; }
        [Parameter] public bool ShowLogo { get; set; } = true;
        [Parameter] public Stock StockToVisualize { get; set; }
        [Parameter] public RenderFragment NullDataPlaceholder { get; set; }
        private StockChartSeriesType chartType = StockChartSeriesType.OHLC;
        private TimeFilter timeFilter = TimeFilter.GetFilters().First();
        private IntervalFilter intervalFilter = IntervalFilter.GetFilters().First();
        private List<StockIntervalDetails> StockDetails => GenerateMockDetails();

        private List<StockIntervalDetails> GenerateMockDetails()
        {
            timeFilter.StartDate = DateTime.Now;
            return StocksRepo.GenerateStockIntervals(StockToVisualize, intervalFilter.GapInMinutes, timeFilter.StartDate.Value.AddHours(-timeFilter.DurationInHours), timeFilter.StartDate.Value);
        }
    }
}
