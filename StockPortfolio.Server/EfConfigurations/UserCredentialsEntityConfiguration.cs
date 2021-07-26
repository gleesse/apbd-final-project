using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.EfConfigurations
{
    public class UserCredentialsEntityConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.ToTable("UserCredentials");
            builder.HasKey(e => e.UserCredentialsID).HasName("UserCredentials_PK");
            builder.Property(e => e.Login).IsRequired().HasMaxLength(100);
            builder.Property(e => e.PasswordHashed).IsRequired().HasMaxLength(256);
            builder.Property(e => e.Salt).IsRequired().HasMaxLength(64);

            builder
                .HasOne(e => e.UserReference)
                .WithOne(user => user.Credentials)
                .HasForeignKey<User>(user => user.CredentialsID);
        }
    }
}
