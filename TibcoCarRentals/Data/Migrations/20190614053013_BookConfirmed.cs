using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class BookConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BookingConfirmed",
                table: "CarBooking",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingConfirmed",
                table: "CarBooking");
        }
    }
}
