using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapitalFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerContributionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Customers",
                newName: "Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CPF",
                table: "Customers",
                newName: "IX_Customers_Cpf");

            migrationBuilder.CreateTable(
                name: "CustomerContributionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContributionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContributionHistories_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContributionHistories_CustomerId",
                table: "CustomerContributionHistories",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContributionHistories_StartDate",
                table: "CustomerContributionHistories",
                column: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContributionHistories");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Customers",
                newName: "CPF");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Cpf",
                table: "Customers",
                newName: "IX_Customers_CPF");
        }
    }
}
