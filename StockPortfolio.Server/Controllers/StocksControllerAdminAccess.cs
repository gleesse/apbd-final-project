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
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/admin/stocks")]
    [ApiController]
    public class StocksControllerAdminAccess : ControllerBase
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksControllerAdminAccess(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<Stock>> GetAsyncAdmin(string symbol)
        {
            var result =  await _stocksRepository.GetAsync(symbol);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Stock>> GetAllAsyncAdmin()
        {
            var result = await _stocksRepository.GetAllAsync();
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> PostAsync([FromBody] StockRequestDTO stockDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var stock = stockDTO.ToModel();
            var result = await _stocksRepository.AddAsync(stock);
            if (result is null) return StatusCode(500);
            await _stocksRepository.SaveAsync();
            return Ok(result);
        }

        [HttpPatch("{stockToUpdateID}")]
        public async Task<ActionResult<Stock>> PatchAsync(int stockToUpdateID, [FromBody] StockRequestDTO stockDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var stock = stockDTO.ToModel();
            stock.StockID = stockToUpdateID;
            var result = await _stocksRepository.UpdateAsync(stockToUpdateID, stock);
            if (result is null) return NotFound();
            await _stocksRepository.SaveAsync();
            return Ok(result);
        }

        [HttpDelete("{stockToDeleteID}")]
        public async Task<ActionResult<Stock>> DeleteAsync(int stockToDeleteID)
        {
            if (stockToDeleteID < 0) return BadRequest();
            var result = await _stocksRepository.DeleteAsync(stockToDeleteID);
            if (result is null) return NotFound();
            await _stocksRepository.SaveAsync();
            return result;
        }
    }
}
