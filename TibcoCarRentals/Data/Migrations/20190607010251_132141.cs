using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class _132141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Booked",
                table: "Bus",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booked",
                table: "Bus");
        }
    }
}
