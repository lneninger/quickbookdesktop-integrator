using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class FixPriceLevelInventoryItemRemoveExtraFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemFullName",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "ItemId",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "INV",
                table: "PriceLevelInventoryItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemFullName",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);
        }
    }
}
