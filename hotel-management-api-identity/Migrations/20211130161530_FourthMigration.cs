using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Room",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasDiscount = table.Column<bool>(type: "bit", nullable: false),
                    CheckOutDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_BookingId",
                table: "Room",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_BookingId",
                table: "Customer",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AmountPaid",
                table: "Booking",
                column: "AmountPaid");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CheckOutDate",
                table: "Booking",
                column: "CheckOutDate");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatedById",
                table: "Booking",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatedOn",
                table: "Booking",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HasDiscount",
                table: "Booking",
                column: "HasDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Id",
                table: "Booking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Booking_BookingId",
                table: "Customer",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Booking_BookingId",
                table: "Room",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Booking_BookingId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Booking_BookingId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Room_BookingId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Customer_BookingId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Customer");
        }
    }
}
