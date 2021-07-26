using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Server.Migrations
{
    public partial class FixedUserStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Stock_Stock_StockReferenceStockID",
                table: "User_Stock");

            migrationBuilder.DropForeignKey(
                name: "User_Stock_Stock",
                table: "User_Stock");

            migrationBuilder.DropIndex(
                name: "IX_User_Stock_StockReferenceStockID",
                table: "User_Stock");

            migrationBuilder.DropColumn(
                name: "StockReferenceStockID",
                table: "User_Stock");

            migrationBuilder.AddForeignKey(
                name: "User_Stock_Stock",
                table: "User_Stock",
                column: "StockID",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "User_Stock_User",
                table: "User_Stock",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "User_Stock_Stock",
                table: "User_Stock");

            migrationBuilder.DropForeignKey(
                name: "User_Stock_User",
                table: "User_Stock");

            migrationBuilder.AddColumn<int>(
                name: "StockReferenceStockID",
                table: "User_Stock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_Stock_StockReferenceStockID",
                table: "User_Stock",
                column: "StockReferenceStockID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Stock_Stock_StockReferenceStockID",
                table: "User_Stock",
                column: "StockReferenceStockID",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "User_Stock_Stock",
                table: "User_Stock",
                column: "StockID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
