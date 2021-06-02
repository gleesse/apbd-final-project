using System;

namespace StockPortfolio.Helpers
{
    public static class Constants
    {
        public const int MaxLabelStepsInStocksChart = 15;

        public static DateTime GetMinDate()
        {
            return DateTime.Now.AddMonths(-3);
        }

        public static DateTime GetMaxDate()
        {
            return DateTime.Now;
        }
    }
}
