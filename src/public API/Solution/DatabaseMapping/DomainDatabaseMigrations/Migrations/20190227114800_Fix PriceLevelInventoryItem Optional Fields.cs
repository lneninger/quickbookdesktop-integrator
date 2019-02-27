using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class FixPriceLevelInventoryItemOptionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Type",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Type",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: false,
                oldClrType: typeof(short),
                oldNullable: true);
        }
    }
}
