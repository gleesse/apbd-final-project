using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Blazor;
using StockPortfolio.Models;
using StockPortfolio.Helpers;

namespace StockPortfolio.Components.Charts
{
    public partial class ChartNavigation
    {
        [Parameter] public StockChartSeriesType ChartType { get; set; }
        [Parameter] public EventCallback<StockChartSeriesType> ChartTypeChanged { get; set; }
        [Parameter] public IntervalFilter ActiveIntervalFilter { get; set; }
        [Parameter] public EventCallback<IntervalFilter> ActiveIntervalFilterChanged { get; set; }
        [Parameter] public TimeFilter ActiveTimeFilter { get; set; }
        [Parameter] public EventCallback<TimeFilter> ActiveTimeFilterChanged { get; set; }

        //data sources
        private List<ChartType> AvailableChartTypes { get; set; } = Models.ChartType.GetAvailableChartTypes();
        private List<TimeFilter> TimeFilters { get; set; } = TimeFilter.GetFilters();
        private List<IntervalFilter> IntervalFilters { get; set; } = IntervalFilter.GetFilters();

        private DateTime MinDate { get; set; } = Constants.GetMinDate();
        private DateTime MaxDate { get; set; } = Constants.GetMaxDate();

        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; } = DateTime.Now;

        protected override void OnInitialized()
        {
            ActiveTimeFilter = TimeFilters.First();
            ActiveTimeFilterChanged.InvokeAsync(ActiveTimeFilter);
            StartDate = EndDate.AddHours(-ActiveTimeFilter.DurationInHours);
            FilterIntervals(ActiveTimeFilter.DurationInHours * 60);

            TimeFilters.Add(new TimeFilter() { Name = "MAX", DurationInHours = (int)(MaxDate - MinDate).TotalHours });
        }

        void DatesChanged()
        {
            if (StartDate.Equals(EndDate)) StartDate = StartDate.AddDays(-1);
            var datesInterval = (int)(EndDate - StartDate).TotalMinutes;

            ActiveTimeFilter = new TimeFilter { StartDate = StartDate, DurationInHours = datesInterval / 60};
            ActiveTimeFilterChanged.InvokeAsync(ActiveTimeFilter);

            FilterIntervals(datesInterval);
        }

        void OnTimeFilterClick(TimeFilter selectedFilter)
        {
            if (ActiveTimeFilter == selectedFilter) return;
            ActiveTimeFilter = selectedFilter;
            ActiveTimeFilter.StartDate = DateTime.Now.AddHours(-ActiveTimeFilter.DurationInHours);
            ActiveTimeFilterChanged.InvokeAsync(ActiveTimeFilter);
            EndDate = MaxDate;
            StartDate = EndDate.AddHours(-selectedFilter.DurationInHours);

            FilterIntervals(selectedFilter.DurationInHours * 60);
        }

        void OnIntervalFilterClick(string selectedFilterName)
        {
            ActiveIntervalFilter = IntervalFilter.GetFilters().First(f => f.Name == selectedFilterName);
            ActiveIntervalFilterChanged.InvokeAsync(ActiveIntervalFilter);
        }

        void FilterIntervals(int durationInMinutes)
        {
            IntervalFilters = IntervalFilter.GetFilters().Where(i => i.GapInMinutes < durationInMinutes && durationInMinutes / i.GapInMinutes <= 24).ToList();
            ActiveIntervalFilter = IntervalFilters.First();
            ActiveIntervalFilterChanged.InvokeAsync(ActiveIntervalFilter);
        }
    }
}
