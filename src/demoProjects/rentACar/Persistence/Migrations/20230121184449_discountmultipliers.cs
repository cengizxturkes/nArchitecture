using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class discountmultipliers : Migration
    {
        //servis ve dal oluştur emen controler ile
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "ProductDiscounts");

            migrationBuilder.AddColumn<bool>(
                name: "Multiplier",
                table: "Discounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "Discounts");

            migrationBuilder.AddColumn<bool>(
                name: "Multiplier",
                table: "ProductDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
