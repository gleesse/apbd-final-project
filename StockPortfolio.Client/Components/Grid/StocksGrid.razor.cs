using Microsoft.AspNetCore.Components;
using StockPortfolio.Services;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockPortfolio.Dispatchers;

namespace StockPortfolio.Components.Grid
{
    public partial class StocksGrid
    {
        [Inject] StocksRepository StocksListService { get; set; }
        [Parameter] public Stock SelectedStock { get; set; }
        IEnumerable<Stock> SelectedStocks
        {
            get
            {
                return StocksListService.GetStocks(true).Result;
            }
        }
        [Parameter] public List<Stock> Data { get; set; }

        [Inject] private WindowResizeDispatcher WindowDispatcher { get; set; }

        protected override void OnInitialized()
        {
            WindowResizeDispatcher.WindowResize += RecalculateGridSize;
            RecalculateGridSize();
        }

        public void Dispose()
        {
            WindowResizeDispatcher.WindowResize -= RecalculateGridSize;
        }

        private async Task RecalculateGridSize()
        {
            var browserDimensions = await WindowDispatcher.GetBrowserWidth();
            LargeBrowserSize = browserDimensions.Width > 1024;
            MediumBrowserSize = browserDimensions.Width > 768;
            SmallBrowserSize = browserDimensions.Width > 640;
            StateHasChanged();
        }

        bool MediumBrowserSize { get; set; }
        bool LargeBrowserSize { get; set; }
        bool SmallBrowserSize { get; set; }
    }
}
