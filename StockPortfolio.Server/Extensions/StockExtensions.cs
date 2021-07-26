using AutoMapper;
using StockPortfolio.Models;
using StockPortfolio.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Extensions
{
    public static class StockExtensions
    {
        public static StockRequestDTO ToDTO(this Stock stock)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Stock, StockRequestDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<StockRequestDTO>(stock);
        }

        public static IEnumerable<StockRequestDTO> ToDTOs(this IEnumerable<Stock> stocks)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Stock, StockRequestDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<StockRequestDTO>>(stocks);
        }

        public static Stock ToModel(this StockRequestDTO stockDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StockRequestDTO, Stock>());
            var mapper = new Mapper(config);
            return mapper.Map<Stock>(stockDTO);
        }
    }
}
