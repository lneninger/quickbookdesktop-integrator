using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainDatabaseMigrations.Migrations
{
    public partial class AddPriceLevelInventoryItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceLevelInventoryItem",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 6, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventoryItemId = table.Column<int>(nullable: false),
                    PriceLevelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ItemId = table.Column<string>(nullable: true),
                    ItemFullName = table.Column<string>(nullable: true),
                    Type = table.Column<short>(nullable: false),
                    CustomPrice = table.Column<decimal>(nullable: true),
                    CustomPricePercent = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevelInventoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLevelInventoryItem_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalSchema: "INV",
                        principalTable: "InventoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLevelInventoryItem_PriceLevel_PriceLevelId",
                        column: x => x.PriceLevelId,
                        principalSchema: "INV",
                        principalTable: "PriceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceLevelInventoryItem_InventoryItemId",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLevelInventoryItem_PriceLevelId",
                schema: "INV",
                table: "PriceLevelInventoryItem",
                column: "PriceLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceLevelInventoryItem",
                schema: "INV");
        }
    }
}
