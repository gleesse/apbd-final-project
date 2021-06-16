using Microsoft.EntityFrameworkCore;
using StockPortfolio.Models;
using StockPortfolio.Server.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationContext _dbContext;
        public UsersRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        public Task<User> GetAsync(string login) => Task.FromResult(_dbContext.Set<User>().Include(u => u.Credentials).Where(u => u.Credentials.Login == login).FirstOrDefault());

        public async Task<User> UpdateAsync(int id, User entity)
        {
            if (id != entity.UserID)
            {
                return null;
            }
            var record = await _dbContext.Set<User>().FindAsync(id);
            if (record is null)
            {
                return null;
            }
            _dbContext.Entry(record).CurrentValues.SetValues(entity);
            return record;
        }

        public async Task<User> AddAsync(User entity)
        {
            if (entity is null) return null;
            var createdEntity = (await _dbContext.Set<User>().AddAsync(entity)).Entity;
            await SaveAsync();
            var createdUser = await _dbContext.Set<User>().Where(u => u.CredentialsID == createdEntity.CredentialsID).FirstOrDefaultAsync();
            var createdCredentials = await _dbContext.Set<UserCredentials>().FindAsync(createdEntity.CredentialsID);
            var credentialsUpdated = createdCredentials;
            credentialsUpdated.UserID = createdUser.UserID;
            credentialsUpdated.UserReference = createdUser;
            _dbContext.Entry(createdCredentials).CurrentValues.SetValues(credentialsUpdated);
            await SaveAsync();
            return createdUser;
        }

        #region Not Implemented
        
        public Task<User> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> GetAsync(string login);
    }
}
