using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.EfConfigurations
{
    public class User_StockEntityConfiguration : IEntityTypeConfiguration<User_Stock>
    {
        public void Configure(EntityTypeBuilder<User_Stock> builder)
        {
            builder.ToTable("User_Stock");
            builder.HasKey(e => new { e.UserID, e.StockID}).HasName("User_Stock_PK");

            builder
                .HasOne(e => e.UserReference)
                .WithMany(user => user.User_Stocks)
                .HasForeignKey(user => user.UserID)
                .HasConstraintName("User_Stock_User");

            builder
                .HasOne(e => e.StockReference)
                .WithMany(stock => stock.User_Stocks)
                .HasForeignKey(stock => stock.StockID)
                .HasConstraintName("User_Stock_Stock");
        }
    }
}
