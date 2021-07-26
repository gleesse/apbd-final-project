using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ceo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    FullTimeEmployees = table.Column<int>(type: "int", nullable: false),
                    DayChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VolumeAvg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfficialURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerEarningRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Stock_PK", x => x.StockID);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    UserCredentialsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHashed = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserCredentials_PK", x => x.UserCredentialsID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefreshToken = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: true),
                    RefreshTokenExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CredentialsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_PK", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_UserCredentials_CredentialsID",
                        column: x => x.CredentialsID,
                        principalTable: "UserCredentials",
                        principalColumn: "UserCredentialsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Stock",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StockID = table.Column<int>(type: "int", nullable: false),
                    StockReferenceStockID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_Stock_PK", x => new { x.UserID, x.StockID });
                    table.ForeignKey(
                        name: "FK_User_Stock_Stock_StockReferenceStockID",
                        column: x => x.StockReferenceStockID,
                        principalTable: "Stock",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "User_Stock_Stock",
                        column: x => x.StockID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CredentialsID",
                table: "User",
                column: "CredentialsID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Stock_StockID",
                table: "User_Stock",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Stock_StockReferenceStockID",
                table: "User_Stock",
                column: "StockReferenceStockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Stock");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserCredentials");
        }
    }
}
