using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class UserCredentials : EntityBase
    {
        public int UserCredentialsID { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHashed { get; set; }
        public byte[] Salt { get; set; }

        public int UserID { get; set; }
        public User UserReference { get; set; }
    }
}
