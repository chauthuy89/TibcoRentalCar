using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class _2121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_BusType_BusTypeId",
                table: "Bus");

            migrationBuilder.DropTable(
                name: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_Bus_BusTypeId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DaysTaken",
                table: "CarBooking");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CarBooking");

            migrationBuilder.DropColumn(
                name: "BusTypeId",
                table: "Bus");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contact",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Contact",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Car",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequirement",
                table: "BusBooking",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Bus",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contact",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Contact",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaysTaken",
                table: "CarBooking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CarBooking",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Car",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequirement",
                table: "BusBooking",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Bus",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "BusTypeId",
                table: "Bus",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusType",
                columns: table => new
                {
                    BusTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusType", x => x.BusTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusTypeId",
                table: "Bus",
                column: "BusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusType_BusTypeId",
                table: "Bus",
                column: "BusTypeId",
                principalTable: "BusType",
                principalColumn: "BusTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
