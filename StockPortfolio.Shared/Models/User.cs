using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }

        public int CredentialsID { get; set; }
        public UserCredentials Credentials { get; set; }

        public ICollection<User_Stock> User_Stocks { get; set; }
    }
}
