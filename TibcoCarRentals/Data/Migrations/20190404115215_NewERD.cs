using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class NewERD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "CarBooking",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarID",
                table: "CarBooking",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarBooking_CarID",
                table: "CarBooking",
                column: "CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBooking_Car_CarID",
                table: "CarBooking",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBooking_Car_CarID",
                table: "CarBooking");

            migrationBuilder.DropIndex(
                name: "IX_CarBooking_CarID",
                table: "CarBooking");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "CarBooking",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CarID",
                table: "CarBooking",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Car",
                nullable: false,
                defaultValue: 0);
        }
    }
}
