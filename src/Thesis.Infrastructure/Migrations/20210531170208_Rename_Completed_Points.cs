using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thesis.Infrastructure.Migrations
{
    public partial class Rename_Completed_Points : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedPoints");

            migrationBuilder.CreateTable(
                name: "CompletedPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    RunId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedPoint_Point_PointId",
                        column: x => x.PointId,
                        principalTable: "Point",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletedPoint_Run_RunId",
                        column: x => x.RunId,
                        principalTable: "Run",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedPoint_PointId",
                table: "CompletedPoint",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedPoint_RunId",
                table: "CompletedPoint",
                column: "RunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedPoint");

            migrationBuilder.CreateTable(
                name: "CompletedPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    RunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedPoints_Point_PointId",
                        column: x => x.PointId,
                        principalTable: "Point",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletedPoints_Run_RunId",
                        column: x => x.RunId,
                        principalTable: "Run",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedPoints_PointId",
                table: "CompletedPoints",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedPoints_RunId",
                table: "CompletedPoints",
                column: "RunId");
        }
    }
}
