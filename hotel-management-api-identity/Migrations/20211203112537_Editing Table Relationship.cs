using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class EditingTableRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Employee_EmployeeId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Employee_EmployeeId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Menu_MenuId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_MenuId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Booking_EmployeeId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MenuId",
                table: "Sales",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_EmployeeId",
                table: "Booking",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Employee_EmployeeId",
                table: "Booking",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Employee_EmployeeId",
                table: "Sales",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Menu_MenuId",
                table: "Sales",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");
        }
    }
}
