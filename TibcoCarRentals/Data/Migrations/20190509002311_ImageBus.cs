using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class ImageBus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLocation",
                table: "Bus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocation",
                table: "Bus");
        }
    }
}
