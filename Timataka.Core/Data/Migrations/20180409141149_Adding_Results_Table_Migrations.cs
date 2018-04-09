using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_Results_Table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    HeatId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Club = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    FinalTime = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => new { x.HeatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Results_Heats_HeatId",
                        column: x => x.HeatId,
                        principalTable: "Heats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Results_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
