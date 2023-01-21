using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductFbaServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GoodsAcceptance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabelDisassembly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FnskuLabeling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalLabeling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolyBagging = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BubbleWrapping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Boxing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReBoxing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Multipack = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shipping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FbaBoxLabeling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaletInOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Palletizing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bundle = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFbaServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFbaServices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFbaServices_UserId",
                table: "ProductFbaServices",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFbaServices");
        }
    }
}
