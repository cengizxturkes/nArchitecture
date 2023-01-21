using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class fbaservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFbaServices_Users_UserId",
                table: "ProductFbaServices");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProductFbaServices",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFbaServices_UserId",
                table: "ProductFbaServices",
                newName: "IX_ProductFbaServices_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFbaServices_Products_ProductId",
                table: "ProductFbaServices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFbaServices_Products_ProductId",
                table: "ProductFbaServices");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductFbaServices",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFbaServices_ProductId",
                table: "ProductFbaServices",
                newName: "IX_ProductFbaServices_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFbaServices_Users_UserId",
                table: "ProductFbaServices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
