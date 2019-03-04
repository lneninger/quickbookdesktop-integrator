using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class FixPriceLevelInventoryItemAddDeleteFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                type: "nvarchar(100)",
                nullable: false,
                defaultValueSql: "SYSTEM_USER");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "INV",
                table: "PriceLevelInventoryItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "INV",
                table: "PriceLevelInventoryItem");
        }
    }
}
