using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Login_Email",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Login",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_Email",
                table: "Login",
                column: "Email");
        }
    }
}
