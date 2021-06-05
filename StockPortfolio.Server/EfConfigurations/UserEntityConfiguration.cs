using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.EfConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(e => e.UserID).HasName("User_PK");
            builder.Property(e => e.FirstName).HasMaxLength(100);
            builder.Property(e => e.LastName).HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);

            builder.Property(e => e.RefreshToken).HasMaxLength(64);
            builder.Property(e => e.RefreshTokenExpirationDate);

            builder
                .HasOne(e => e.Credentials)
                .WithOne(creds => creds.UserReference)
                .HasForeignKey<UserCredentials>(creds => creds.UserID);
        }
    }
}
