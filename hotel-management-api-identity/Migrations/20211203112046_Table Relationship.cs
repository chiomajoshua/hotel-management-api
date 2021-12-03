using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class TableRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MenuId",
                table: "Sales",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Menu_MenuId",
                table: "Sales",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Menu_MenuId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_MenuId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Sales");
        }
    }
}
