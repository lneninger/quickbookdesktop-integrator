using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class AddInventoryAssetsAccounttoInventoryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "AssetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_IncomeAccount_AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "AssetAccountId",
                principalSchema: "INV",
                principalTable: "IncomeAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_IncomeAccount_AssetAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_AssetAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "AssetAccountId",
                schema: "INV",
                table: "InventoryItem");
        }
    }
}
