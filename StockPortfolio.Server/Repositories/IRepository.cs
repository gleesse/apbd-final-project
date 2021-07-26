using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(int id, T entity);
        public Task<T> DeleteAsync(int id);
        public Task SaveAsync();
    }
}
