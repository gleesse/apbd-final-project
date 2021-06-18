using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockPortfolio.Helpers;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Services
{
    public class StockMarketApiProviderService : IStocksInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private string MainProvider_API_KEY => _config["Server:ExternalApi:PolygonApiKey"];
        private string AdditionalProvider_API_KEY => _config["Server:ExternalApi:AlphaVantageApiKey"];

        public StockMarketApiProviderService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _config = config;
        }

        public async Task<IEnumerable<SearchMatchStockModel>> GetBestMatchesAsync(string keyword)
        {
            if (keyword is null || keyword.Length < 1) return null;
            var url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={keyword}&apikey={AdditionalProvider_API_KEY}";
            string detailsJson;
            if ((detailsJson = await SendGetRequestAsync(url)) != null)
            {
                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "YYYY-MM-DD",
                    ContractResolver = new StockContractResolver()
                };
                var res = JsonConvert.DeserializeObject<IEnumerable<SearchMatchStockModel>>(detailsJson[21..^1].Trim(), settings);
                return res;
            }
            return null;
        }

        public async Task<Stock> GetStockAsync(string symbol)
        {
            string body1 = await SendGetRequestAsync($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={AdditionalProvider_API_KEY}");
            string body2 = await SendGetRequestAsync($"https://api.polygon.io/v1/meta/symbols/{symbol}/company?&apiKey={MainProvider_API_KEY}");
            if (body1 is null || body1.Length < 1 || body1 == "{}" || body2 is null || body2.Length < 1 || body2 == "{}") return null;
            body1 = body1[21..^1].Trim(); //such workaround coz alphavantage sends us properties as an 1 array and i need to transform in into simple json consisting of variables"
            var jObject1 = JObject.Parse(body1);
            jObject1.Property("10. change percent").Value = jObject1.Property("10. change percent").Value.ToString()[0..^1]; //to remove % sign in the end of that property
            var jObject2 = JObject.Parse(body2);

            var result = new JObject();
            result.Merge(jObject1);
            result.Merge(jObject2);

            var settings = new JsonSerializerSettings
            {
                DateFormatString = "YYYY-MM-DD",
                ContractResolver = new StockContractResolver()
            };
            try
            {
                return JsonConvert.DeserializeObject<Stock>(result.ToString(), settings);
            }
            catch { return null; }
        }

        public async Task<StockIntervalDetails> GetStockIntervalDetailsAsync(string symbol)
        {
            StockIntervalDetails details;
            string detailsJson;
            var url = $"https://api.polygon.io/v1/open-close/{symbol}/2020-10-14?unadjusted=true&apiKey={MainProvider_API_KEY}";
            if ((detailsJson = await SendGetRequestAsync(url)) != null)
            {
                details = JsonConvert.DeserializeObject<StockIntervalDetails>(detailsJson);
                details.Date = DateTime.Now;
                return details;
            }
            return null;
        }

        private async Task<string> SendGetRequestAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }
    }
}
