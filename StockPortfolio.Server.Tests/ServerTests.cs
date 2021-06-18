using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StockPortfolio.Helpers;
using StockPortfolio.Models;
using StockPortfolio.Server.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Tests
{
    public class ServerTests
    {
        class StocksInfoServiceTests
        {
            IConfiguration Configuration { get; set; }
            private string AlphaVantageApiKey => Configuration["Server:ExternalApi:AlphaVantageApiKey"];

            public StocksInfoServiceTests()
            {
                var builder = new ConfigurationBuilder()
                    .AddUserSecrets<StocksInfoServiceTests>();

                Configuration = builder.Build();
            }

            [SetUp]
            public void Setup()
            {
            }

            [Test]
            public async Task GetStockDetailsFromStocksInfoService_ReturnsStockDetailsObject()
            {
                IStocksInfoService stocksService = new StockMarketApiProviderService(Configuration);
                var result = await stocksService.GetStockIntervalDetailsAsync("AAPL");
                System.Console.WriteLine(result);
                Assert.NotNull(result);
            }

            [Test]
            public async Task GetPriceInfo_ReturnsStockPrice()
            {
                string body1 = "";
                string body2 = "";
                var client = new HttpClient();
                var symbol = "TSLA";
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={AlphaVantageApiKey}")
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    body1 = body;
                }
                
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.polygon.io/v1/meta/symbols/{symbol}/company?&apiKey=YsGa_QoCBcfQAyjEH4m3hkUAgs4ApsgN")
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    body2 = body;
                }

                body1 = body1[21..^1].Trim();
                Console.WriteLine(body1);
                var jObject1 = JObject.Parse(body1);
                jObject1.Property("10. change percent").Value = jObject1.Property("10. change percent").Value.ToString()[0..^1];
                Console.WriteLine(jObject1.ToString());
                var jObject2 = JObject.Parse(body2);

                var result = new JObject();

                result.Merge(jObject1);
                result.Merge(jObject2);

                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "YYYY-MM-DD",
                    ContractResolver = new StockContractResolver()
                };
                var stock = JsonConvert.DeserializeObject<Stock>(result.ToString(),settings);
                Console.WriteLine($"price: {stock.Price}, industry: {stock.Industry}");
                Assert.NotNull(stock);
            }

            [Test]
            public async Task GetBestMatches_ShouldReturnBestMatcherForKeywoard_TSLA()
            {
                IStocksInfoService service = new StockMarketApiProviderService(Configuration);
                var res = await service.GetBestMatchesAsync("TSLA");
                foreach (var item in res)
                {
                    Console.WriteLine(item.Name+" "+item.Symbol);
                }
                Assert.NotNull(res);
            }
        }
    }
}