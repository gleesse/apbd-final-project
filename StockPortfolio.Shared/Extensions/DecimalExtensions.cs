using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToString(this decimal @decimal, bool compactFormat, bool includePlusSign = false)
        {
            if (@decimal == 0) return "0";
            
            string returnValue = @decimal.ToString();
            if (compactFormat)
            {
                var absValue = Math.Abs(@decimal);
                returnValue = @decimal.ToString("0.##");

                if (absValue >= 1000000000000)
                    returnValue = (@decimal / 1000000000000).ToString("0.##") + "T";
                else if (absValue >= 1000000000)
                    returnValue = (@decimal / 1000000000).ToString("0.##") + "B";
                else if (absValue >= 1000000)
                    returnValue = (@decimal / 1000000).ToString("0.##") + "M";
                else if (absValue >= 1000)
                    returnValue = (@decimal / 1000).ToString("0.##") + "K";
            }
            if (includePlusSign && @decimal > 0) return '+' + returnValue;
            return returnValue;
        }
    }
}
