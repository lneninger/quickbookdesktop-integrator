using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class AddAccountHerarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_IncomeAccount_AssetAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_IncomeAccount_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeAccount",
                schema: "INV",
                table: "IncomeAccount");

            //Clean Tables
            migrationBuilder.Sql("TRUNCATE TABLE [INV].[InventoryItem]");
            migrationBuilder.Sql("TRUNCATE TABLE [INV].[IncomeAccount]");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_AssetAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropIndex(
               name: "IX_InventoryItem_IncomeAccountId",
               schema: "INV",
               table: "InventoryItem");

            migrationBuilder.RenameTable(
                name: "IncomeAccount",
                schema: "INV",
                newName: "Account",
                newSchema: "INV");

            migrationBuilder.AlterColumn<int>(
                name: "IncomeAccountId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.DropColumn(name: "Id", schema: "INV",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "INV",
                table: "Account",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "AccountTypeId",
                schema: "INV",
                table: "Account",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "INV",
                table: "Account",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                schema: "INV",
                table: "Account",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccountType",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "INV",
                table: "AccountType",
                columns: new[] { "Id", "Name" },
                values: new object[] { "INCOME", "Income Account" });

            migrationBuilder.InsertData(
                schema: "INV",
                table: "AccountType",
                columns: new[] { "Id", "Name" },
                values: new object[] { "INVENTORY", "Inventory Account" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountTypeId",
                schema: "INV",
                table: "Account",
                column: "AccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AccountType_AccountTypeId",
                schema: "INV",
                table: "Account",
                column: "AccountTypeId",
                principalSchema: "INV",
                principalTable: "AccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_Account_AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "AssetAccountId",
                principalSchema: "INV",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_Account_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "IncomeAccountId",
                principalSchema: "INV",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountType_AccountTypeId",
                schema: "INV",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_Account_AssetAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_Account_IncomeAccountId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropTable(
                name: "AccountType",
                schema: "INV");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                schema: "INV",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_AccountTypeId",
                schema: "INV",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "INV",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                schema: "INV",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "INV",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                schema: "INV",
                newName: "IncomeAccount",
                newSchema: "INV");

            migrationBuilder.AlterColumn<string>(
                name: "IncomeAccountId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.DropColumn(name: "Id", schema: "INV",
               table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "INV",
                table: "IncomeAccount",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeAccount",
                schema: "INV",
                table: "IncomeAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_IncomeAccount_AssetAccountId",
                schema: "INV",
                table: "InventoryItem",
                column: "AssetAccountId",
                principalSchema: "INV",
                principalTable: "IncomeAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
