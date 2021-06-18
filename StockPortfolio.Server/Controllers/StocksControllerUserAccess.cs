using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Models;
using StockPortfolio.Server.Extensions;
using StockPortfolio.Server.Models;
using StockPortfolio.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Controllers
{
    [Authorize]
    [Route("api/user/stocks")]
    [ApiController]
    public class StocksControllerUserAccess : ControllerBase
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksControllerUserAccess(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<StockRequestDTO>> GetAsyncUser(string symbol)
        {
            var result = await _stocksRepository.GetAsync(symbol);
            if (result is null) return NotFound();
            return Ok(result.ToDTO());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequestDTO>>> GetAllAsyncUser()
        {
            var result = await _stocksRepository.GetAllAsync();
            if (result is null) return NotFound();
            return Ok(result.ToDTOs());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SearchMatchStockModel>>> GetBestMatchesAsyncUser([FromQuery(Name = "keyword")] string keyword)
        {
            return Ok((await _stocksRepository.GetBestMatchesAsync(keyword)));
        }

        [HttpGet("{symbol}/details")]
        public async Task<ActionResult<StockIntervalDetails>> GetIntervalDetailsAsyncUser(string symbol)
        {
            return Ok(await _stocksRepository.GetIntervalDetailsAsync(symbol));
        }
    }
}
