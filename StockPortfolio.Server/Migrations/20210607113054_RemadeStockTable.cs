using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Server.Migrations
{
    public partial class RemadeStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Stock",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Stock");
        }
    }
}
