using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class ChipInHea_and_Time_tables_changes_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChipsInHeats_AspNetUsers_UserId",
                table: "ChipsInHeats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Times_ChipCode_HeatId",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChipsInHeats",
                table: "ChipsInHeats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_HeatId_UserId",
                table: "ChipsInHeats");

            migrationBuilder.DropIndex(
                name: "IX_ChipsInHeats_HeatId",
                table: "ChipsInHeats");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ChipsInHeats",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Times_ChipCode_HeatId_TimeNumber",
                table: "Times",
                columns: new[] { "ChipCode", "HeatId", "TimeNumber" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                columns: new[] { "HeatId", "ChipCode", "TimeNumber" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_HeatId",
                table: "ChipsInHeats",
                columns: new[] { "ChipCode", "HeatId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChipsInHeats",
                table: "ChipsInHeats",
                columns: new[] { "HeatId", "ChipCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ChipsInHeats_UserId",
                table: "ChipsInHeats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChipsInHeats_AspNetUsers_UserId",
                table: "ChipsInHeats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChipsInHeats_AspNetUsers_UserId",
                table: "ChipsInHeats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Times_ChipCode_HeatId_TimeNumber",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_HeatId",
                table: "ChipsInHeats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChipsInHeats",
                table: "ChipsInHeats");

            migrationBuilder.DropIndex(
                name: "IX_ChipsInHeats_UserId",
                table: "ChipsInHeats");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ChipsInHeats",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Times_ChipCode_HeatId",
                table: "Times",
                columns: new[] { "ChipCode", "HeatId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                columns: new[] { "HeatId", "ChipCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChipsInHeats",
                table: "ChipsInHeats",
                columns: new[] { "UserId", "ChipCode" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ChipsInHeats_ChipCode_HeatId_UserId",
                table: "ChipsInHeats",
                columns: new[] { "ChipCode", "HeatId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ChipsInHeats_HeatId",
                table: "ChipsInHeats",
                column: "HeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChipsInHeats_AspNetUsers_UserId",
                table: "ChipsInHeats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
