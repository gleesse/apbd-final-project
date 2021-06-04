using System;
using System.Collections.Generic;
using System.Linq;
using StockPortfolio.Models;
using StockPortfolio.Services;
using System.Timers;
using Microsoft.AspNetCore.Components;

namespace StockPortfolio.Components.Search
{
    public partial class MarketSearch
    {
        [Inject] private StocksRepository StocksRepo { get; set; }
        [Parameter] public Stock SelectedStock { get; set; }
        [Parameter] public EventCallback<Stock> SelectedStockChanged { get; set; }
        private List<Stock> PossibleStocks { get; set; }
        private string searchText;
        private string searchTextDelayed;
        private Timer delayTimer;
        private const int TIMER_DELAY = 300;
        private bool isCursorInsideDropdown = false;

        protected override void OnInitialized()
        {
            delayTimer = new Timer(TIMER_DELAY);
            delayTimer.Elapsed += OnUserStopTyping; //no need to unsubscribe because delayTimer wont overlive searchcomponent (they will dispose together)
            delayTimer.AutoReset = false;
        }

        private void OnKeyUp()
        {
            delayTimer.Stop();
            delayTimer.Start();
        }

        private void HandleInputFieldFocusOut()
        {
            if (!isCursorInsideDropdown) CloseDropdownList();
        }

        private void RegisterCursorInsideDropdownList()
        {
            isCursorInsideDropdown = true;
        }

        private void RegisterCursorOutsideDropdownList()
        {
            isCursorInsideDropdown = false;
        }

        private void SelectStock(Stock selectedStock)
        {
            SelectedStock = selectedStock;
            SelectedStockChanged.InvokeAsync(selectedStock);
            CloseDropdownList();
        }

        private void CloseDropdownList()
        {
            PossibleStocks = null;
            isCursorInsideDropdown = false;
        }

        private void GetPossibleStocks()
        {
            if (searchTextDelayed is null) searchTextDelayed = "A";
            PossibleStocks = StocksRepo.GetAll().Where(s => s.Name.StartsWith(searchTextDelayed, StringComparison.OrdinalIgnoreCase)).ToList().Take(3).ToList();
        }

        private void OnUserStopTyping(Object source, ElapsedEventArgs e)
        {
            /*
            need to wrap it into InvokeAsync Timer.Elapsed is a non-ui event(thread-safety)
            */
            InvokeAsync(() =>
            {
                searchTextDelayed = searchText;
                GetPossibleStocks();
                StateHasChanged();
            });
        }
    }
}
