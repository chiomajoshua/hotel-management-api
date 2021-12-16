using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class SecondTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_SalesId",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_SalesId",
                table: "SaleDetails");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "SaleDetails");

            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "SaleDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "SaleDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "SalesId",
                table: "SaleDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SalesId",
                table: "SaleDetails",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_SalesId",
                table: "SaleDetails",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id");
        }
    }
}
