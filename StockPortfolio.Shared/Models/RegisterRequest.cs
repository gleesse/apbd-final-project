﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
