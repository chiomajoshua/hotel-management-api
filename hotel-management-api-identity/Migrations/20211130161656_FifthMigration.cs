using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Booking_BookingId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Booking_BookingId",
                table: "Room");

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

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CustomerId",
                table: "Booking",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomId",
                table: "Booking",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Customer_CustomerId",
                table: "Booking",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Room_RoomId",
                table: "Booking",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Customer_CustomerId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Room_RoomId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CustomerId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_RoomId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Booking");

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

            migrationBuilder.CreateIndex(
                name: "IX_Room_BookingId",
                table: "Room",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_BookingId",
                table: "Customer",
                column: "BookingId");

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
    }
}
