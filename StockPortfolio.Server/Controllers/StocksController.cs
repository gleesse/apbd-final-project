using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Models;
using StockPortfolio.Server.Models;
using StockPortfolio.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksController(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<Stock>> GetAsync(string symbol)
        {
            var record = await _stocksRepository.GetAsync(symbol);
            if (record is null) return NotFound();
            return Ok(record);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllAsync()
        {
            var records = await _stocksRepository.GetAllAsync();
            if (records is null) return NotFound();
            return Ok(records);
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> PostAsync([FromBody] Stock stock)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _stocksRepository.AddAsync(stock);
            if (result is null) return StatusCode(500);
            await _stocksRepository.SaveAsync();
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<Stock>> PatchAsync([FromQuery] int stockToUpdateID, [FromBody] Stock stock)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _stocksRepository.UpdateAsync(stockToUpdateID, stock);
            if (result is null) return BadRequest();
            await _stocksRepository.SaveAsync();
            return Ok(result);
        }
    }
}
