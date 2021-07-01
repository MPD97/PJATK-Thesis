using Microsoft.EntityFrameworkCore.Migrations;

namespace Thesis.Infrastructure.Migrations
{
    public partial class Next_Point : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextPointId",
                table: "Point",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Point_NextPointId",
                table: "Point",
                column: "NextPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Point_NextPointId",
                table: "Point",
                column: "NextPointId",
                principalTable: "Point",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Point_Point_NextPointId",
                table: "Point");

            migrationBuilder.DropIndex(
                name: "IX_Point_NextPointId",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "NextPointId",
                table: "Point");
        }
    }
}
