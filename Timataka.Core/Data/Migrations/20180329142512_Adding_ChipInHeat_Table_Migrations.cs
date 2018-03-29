using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_ChipInHeat_Table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChipsInHeats",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ChipCode = table.Column<string>(nullable: false),
                    HeatId = table.Column<int>(nullable: false),
                    Valid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChipsInHeats", x => new { x.UserId, x.ChipCode });
                    table.UniqueConstraint("AK_ChipsInHeats_ChipCode_UserId", x => new { x.ChipCode, x.UserId });
                    table.ForeignKey(
                        name: "FK_ChipsInHeats_Chips_ChipCode",
                        column: x => x.ChipCode,
                        principalTable: "Chips",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChipsInHeats_Heats_HeatId",
                        column: x => x.HeatId,
                        principalTable: "Heats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChipsInHeats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChipsInHeats_HeatId",
                table: "ChipsInHeats",
                column: "HeatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChipsInHeats");
        }
    }
}
