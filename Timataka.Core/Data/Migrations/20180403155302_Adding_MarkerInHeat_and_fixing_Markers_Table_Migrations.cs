using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_MarkerInHeat_and_fixing_Markers_Table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Heats_HeatId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Markers_HeatId",
                table: "Markers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_UserId",
                table: "ChipsInHeats");

            migrationBuilder.DropColumn(
                name: "HeatId",
                table: "Markers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_HeatId_UserId",
                table: "ChipsInHeats",
                columns: new[] { "ChipCode", "HeatId", "UserId" });

            migrationBuilder.CreateTable(
                name: "MarkersInHeats",
                columns: table => new
                {
                    HeatId = table.Column<int>(nullable: false),
                    MarkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkersInHeats", x => new { x.HeatId, x.MarkerId });
                    table.ForeignKey(
                        name: "FK_MarkersInHeats_Heats_HeatId",
                        column: x => x.HeatId,
                        principalTable: "Heats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MarkersInHeats_Markers_MarkerId",
                        column: x => x.MarkerId,
                        principalTable: "Markers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarkersInHeats_MarkerId",
                table: "MarkersInHeats",
                column: "MarkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarkersInHeats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_HeatId_UserId",
                table: "ChipsInHeats");

            migrationBuilder.AddColumn<int>(
                name: "HeatId",
                table: "Markers",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_UserId",
                table: "ChipsInHeats",
                columns: new[] { "ChipCode", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Markers_HeatId",
                table: "Markers",
                column: "HeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Heats_HeatId",
                table: "Markers",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
