using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Creating_Table_ContestantInHeat_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContestantsInHeats",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    HeatId = table.Column<int>(nullable: false),
                    Bib = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Team = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestantsInHeats", x => new { x.UserId, x.HeatId });
                    table.UniqueConstraint("AK_ContestantsInHeats_HeatId_UserId", x => new { x.HeatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContestantsInHeats_Heats_HeatId",
                        column: x => x.HeatId,
                        principalTable: "Heats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContestantsInHeats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestantsInHeats");
        }
    }
}
