using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class PriceLevelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceLevel",
                schema: "INV",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValueSql: "SYSTEM_USER"),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Id = table.Column<int>(maxLength: 6, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PriceLevelType = table.Column<string>(maxLength: 15, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PriceLevelPercentage = table.Column<decimal>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceLevel",
                schema: "INV");
        }
    }
}
