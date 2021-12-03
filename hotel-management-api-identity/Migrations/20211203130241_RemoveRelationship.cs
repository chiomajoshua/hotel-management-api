using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class RemoveRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Login_LoginId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_LoginId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LoginId",
                table: "Employee",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LoginId",
                table: "Employee",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Login_LoginId",
                table: "Employee",
                column: "LoginId",
                principalTable: "Login",
                principalColumn: "Id");
        }
    }
}
