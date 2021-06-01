using System;
using Microsoft.AspNetCore.Components;
using StockPortfolio.Dispatchers;
using StockPortfolio.Models;
using StockPortfolio.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace StockPortfolio.Components.Charts
{
    public partial class PieChart
    {
        [Inject] private StocksRepository StocksRepo { get; set; }
        [Parameter] public bool ChartLabelsVisible { get; set; } = false;
        private TelerikChart ChartRef;
        private List<Stock> Data { get; set; }

        protected override void OnInitialized()
        {
            WindowResizeDispatcher.WindowResize += ResizeChart;
            StocksRepo.DataChanged += OnDataChanged;
            Data = StocksRepo.GetStocks(true);
        }

        public void Dispose()
        {
            WindowResizeDispatcher.WindowResize -= ResizeChart;
            StocksRepo.DataChanged -= OnDataChanged;
        }
        
        private void OnDataChanged()
        {
            Data = StocksRepo.GetStocks(true);
            StateHasChanged();
        }

        private async Task ResizeChart()
        {
            ChartRef.Refresh();
        }
    }
}
