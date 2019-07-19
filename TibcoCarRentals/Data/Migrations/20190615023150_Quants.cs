using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class Quants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booked",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Booked",
                table: "Bus");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Car",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Bus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Bus");

            migrationBuilder.AddColumn<bool>(
                name: "Booked",
                table: "Car",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Booked",
                table: "Bus",
                nullable: false,
                defaultValue: false);
        }
    }
}
