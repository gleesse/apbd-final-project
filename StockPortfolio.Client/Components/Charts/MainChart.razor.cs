using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockPortfolio.Models;
using StockPortfolio.Services;
using StockPortfolio.Dispatchers;
using Telerik.Blazor.Components;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor;

namespace StockPortfolio.Components.Charts
{
    public partial class MainChart : ComponentBase
    {
        [Parameter] public List<StockIntervalDetails> ChartData { get; set; }
        [Parameter] public StockChartSeriesType ChartType { get; set; }
        [Parameter] public int ChartLabelStep { get; set; } = 4;
        [Parameter] public IntervalFilter IntervalFilter { get; set; }
        private TelerikStockChart StockChartRef { get; set; }

        protected override void OnInitialized()
        {
            WindowResizeDispatcher.WindowResize += ResizeChart;
        }

        public void Dispose()
        {
            WindowResizeDispatcher.WindowResize -= ResizeChart;
        }

        protected async Task ResizeChart()
        {
            StockChartRef.Refresh();
        }
    }
}
