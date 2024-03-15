using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrencyHistorical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Volume24h = table.Column<double>(type: "float", nullable: true),
                    PercentChange1h = table.Column<double>(type: "float", nullable: true),
                    PercentChange24h = table.Column<double>(type: "float", nullable: true),
                    PercentChange7d = table.Column<double>(type: "float", nullable: true),
                    PercentChange30d = table.Column<double>(type: "float", nullable: true),
                    MarketCap = table.Column<double>(type: "float", nullable: true),
                    TotalSupply = table.Column<double>(type: "float", nullable: true),
                    CirculatingSupply = table.Column<double>(type: "float", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencyHistorical", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekCryptoCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    Percent24h = table.Column<double>(type: "float", nullable: false),
                    Percent7d = table.Column<double>(type: "float", nullable: false),
                    Percent30d = table.Column<double>(type: "float", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    PurchaseDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CryptoCurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekCryptoCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekCryptoCurrency_CryptoCurrency_CryptoCurrencyId",
                        column: x => x.CryptoCurrencyId,
                        principalTable: "CryptoCurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeekCryptoCurrency_CryptoCurrencyId",
                table: "WeekCryptoCurrency",
                column: "CryptoCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoCurrencyHistorical");

            migrationBuilder.DropTable(
                name: "WeekCryptoCurrency");

            migrationBuilder.DropTable(
                name: "CryptoCurrency");
        }
    }
}
