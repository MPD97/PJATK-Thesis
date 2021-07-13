using Microsoft.EntityFrameworkCore.Migrations;

namespace Thesis.Infrastructure.Migrations
{
    public partial class User_Agent_Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceBrand",
                table: "UserAgent",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DeviceIsSpider",
                table: "UserAgent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeviceModel",
                table: "UserAgent",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceBrand",
                table: "UserAgent");

            migrationBuilder.DropColumn(
                name: "DeviceIsSpider",
                table: "UserAgent");

            migrationBuilder.DropColumn(
                name: "DeviceModel",
                table: "UserAgent");
        }
    }
}
