using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class AddIncomeAccounttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Stock",
                schema: "INV",
                table: "InventoryItem",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "SalesPrice",
                schema: "INV",
                table: "InventoryItem",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "IncomeAccountId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IncomeAccount",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    FullName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAccount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "IncomeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_IncomeAccount_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "IncomeAccountId",
                principalSchema: "INV",
                principalTable: "IncomeAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_IncomeAccount_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropTable(
                name: "IncomeAccount",
                schema: "INV");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "IncomeAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.AlterColumn<decimal>(
                name: "Stock",
                schema: "INV",
                table: "InventoryItem",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SalesPrice",
                schema: "INV",
                table: "InventoryItem",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
