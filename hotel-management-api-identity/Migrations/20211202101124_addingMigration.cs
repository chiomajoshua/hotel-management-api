using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class addingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Employee_EmployeeId",
                table: "Login");

            migrationBuilder.DropIndex(
                name: "IX_Login_EmployeeId",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Login");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Login",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Login");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Login",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_EmployeeId",
                table: "Login",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Employee_EmployeeId",
                table: "Login",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
