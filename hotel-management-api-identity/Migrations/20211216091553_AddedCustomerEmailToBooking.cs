using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    public partial class AddedCustomerEmailToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Booking");
        }
    }
}
