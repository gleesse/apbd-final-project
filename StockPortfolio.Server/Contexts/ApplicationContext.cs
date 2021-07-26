using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockPortfolio.Models;
using StockPortfolio.Server.EfConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User_Stock> User_Stocks { get; set; }
        public DbSet<UserCredentials> User_Credentials { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration["Server:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StockEntityConfiguration());
            modelBuilder.ApplyConfiguration(new User_StockEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialsEntityConfiguration());
        }
    }
}
