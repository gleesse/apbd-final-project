﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class StockIntervalDetails //Daily Open/Close stock api
    {
        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal Close { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Volume { get; set; }
    }
}
