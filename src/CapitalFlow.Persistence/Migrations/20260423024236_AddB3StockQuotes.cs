using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapitalFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddB3StockQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Baskets_Name",
                table: "Baskets");

            migrationBuilder.CreateTable(
                name: "B3StockQuotes",
                columns: table => new
                {
                    TradingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ticker = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BdiCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarketType = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OpenPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HighPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LowPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TradedQuantity = table.Column<long>(type: "bigint", nullable: false),
                    TradedVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B3StockQuotes", x => new { x.Ticker, x.TradingDate });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_Name",
                table: "Baskets",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_Status",
                table: "Baskets",
                column: "Status",
                unique: true,
                filter: "Status = true");

            migrationBuilder.CreateIndex(
                name: "IX_B3StockQuotes_Ticker_TradingDate",
                table: "B3StockQuotes",
                columns: new[] { "Ticker", "TradingDate" },
                descending: new[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "B3StockQuotes");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_Name",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_Status",
                table: "Baskets");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_Name",
                table: "Baskets",
                column: "Name",
                unique: true);
        }
    }
}
