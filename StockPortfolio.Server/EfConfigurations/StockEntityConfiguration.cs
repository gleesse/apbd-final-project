using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Server.EfConfigurations
{
    public class StockEntityConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stock");
            builder.HasKey(e => e.StockID).HasName("Stock_PK");
            builder.Property(e => e.Symbol).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(512);
            builder.Property(e => e.Ceo).HasMaxLength(512);
            builder.Property(e => e.Industry).HasMaxLength(512);
            builder.Property(e => e.OfficialURL);
            builder.Property(e => e.LogoImageURL);
            builder.Property(e => e.Price);
            builder.Property(e => e.FullTimeEmployees);
            builder.Property(e => e.DayChange);
            builder.Property(e => e.ChangePercentage);
            builder.Property(e => e.VolumeAvg);
            builder.Property(e => e.MarketCap);
            builder.Property(e => e.PricePerEarningRatio);
        }
    }
}
