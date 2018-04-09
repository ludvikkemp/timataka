using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_Times_Table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    HeatId = table.Column<int>(nullable: false),
                    ChipCode = table.Column<string>(nullable: false),
                    RawTime = table.Column<int>(nullable: false),
                    TimeNumber = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => new { x.HeatId, x.ChipCode });
                    table.UniqueConstraint("AK_Times_ChipCode_HeatId", x => new { x.ChipCode, x.HeatId });
                    table.ForeignKey(
                        name: "FK_Times_Chips_ChipCode",
                        column: x => x.ChipCode,
                        principalTable: "Chips",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Times_Heats_HeatId",
                        column: x => x.HeatId,
                        principalTable: "Heats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Times");
        }
    }
}
