using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Server.Migrations
{
    public partial class RemadeStockModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerEarningRatio",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "VolumeAvg",
                table: "Stock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerEarningRatio",
                table: "Stock",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeAvg",
                table: "Stock",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
