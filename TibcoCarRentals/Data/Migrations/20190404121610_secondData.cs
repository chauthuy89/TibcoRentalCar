using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TibcoCarRentals.Data.Migrations
{
    public partial class secondData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    BusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    DailyPrice = table.Column<double>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    NumSeats = table.Column<int>(nullable: false),
                    Warrent = table.Column<DateTime>(nullable: false),
                    Registration = table.Column<DateTime>(nullable: false),
                    BusTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.BusID);
                    table.ForeignKey(
                        name: "FK_Bus_BusType_BusTypeId",
                        column: x => x.BusTypeId,
                        principalTable: "BusType",
                        principalColumn: "BusTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusBooking",
                columns: table => new
                {
                    BusBookingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumOfGuest = table.Column<int>(nullable: false),
                    DateToBook = table.Column<DateTime>(nullable: false),
                    SpecialRequirement = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    BusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusBooking", x => x.BusBookingID);
                    table.ForeignKey(
                        name: "FK_BusBooking_Bus_BusID",
                        column: x => x.BusID,
                        principalTable: "Bus",
                        principalColumn: "BusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusTypeId",
                table: "Bus",
                column: "BusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusBooking_BusID",
                table: "BusBooking",
                column: "BusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusBooking");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "BusType");
        }
    }
}
