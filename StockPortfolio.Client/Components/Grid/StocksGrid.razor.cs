using Microsoft.AspNetCore.Components;
using StockPortfolio.Dispatchers;
using StockPortfolio.Models;
using StockPortfolio.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace StockPortfolio.Components.Grid
{
    public partial class StocksGrid
    {
        [Inject] StocksRepository StocksRepo { get; set; }
        [Inject] private WindowResizeDispatcher WindowDispatcher { get; set; }
        [Parameter] public int PageSize { get; set; } = 10;
        private List<Stock> Data { get; set; }
        private bool MediumBrowserSize { get; set; }
        private bool LargeBrowserSize { get; set; }
        private bool SmallBrowserSize { get; set; }

        private async Task HandleDelete(GridCommandEventArgs args)
        {
            var stockToDelete = (Stock)args.Item;

            await StocksRepo.DeleteAsync(stockToDelete);
            Data.Remove(stockToDelete);
        }

        protected override void OnInitialized()
        {
            WindowResizeDispatcher.WindowResize += RecalculateGridSize;
            Data = StocksRepo.GetStocks(true);
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
            SmallBrowserSize = browserDimensions.Width > 400;
            StateHasChanged();
        }
    }
}
