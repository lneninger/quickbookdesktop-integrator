using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class Newlastintegrationprocessid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "PriceLevel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "PriceLevel");

            migrationBuilder.DropColumn(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "LastIntegrationProcessId",
                schema: "INV",
                table: "Account");
        }
    }
}
