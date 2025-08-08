using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    public partial class AddCompositeIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adicionar índices compostos para otimizar queries comuns
            migrationBuilder.CreateIndex(
                name: "IX_Sales_Customer_Date",
                table: "Sales",
                columns: new[] { "CustomerId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Branch_Date",
                table: "Sales",
                columns: new[] { "BranchId", "Date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_Customer_Date",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_Branch_Date",
                table: "Sales");
        }
    }
}
