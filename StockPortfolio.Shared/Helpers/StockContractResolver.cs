using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio.Helpers
{
    public class StockContractResolver : DefaultContractResolver
    {
        private Dictionary<string,string> PropertyMappings { get; set; }

        public StockContractResolver()
        {
            PropertyMappings = new()
            {
                { "Symbol", "1. symbol" },
                { "Name", "2. name" } ,
                { "Price", "05. price" },
                { "DayChange", "09. change" },
                { "ChangePercentage", "10. change percent" }
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var result = PropertyMappings.TryGetValue(propertyName, out string propertyValue);
            return result ? propertyValue : base.ResolvePropertyName(propertyName);
        }
    }
}
