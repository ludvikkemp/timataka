using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Chips_table_changes_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chips_CompetitionInstances_LastCompetitionInstanceId",
                table: "Chips");

            migrationBuilder.AlterColumn<int>(
                name: "LastCompetitionInstanceId",
                table: "Chips",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Chips_CompetitionInstances_LastCompetitionInstanceId",
                table: "Chips",
                column: "LastCompetitionInstanceId",
                principalTable: "CompetitionInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chips_CompetitionInstances_LastCompetitionInstanceId",
                table: "Chips");

            migrationBuilder.AlterColumn<int>(
                name: "LastCompetitionInstanceId",
                table: "Chips",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chips_CompetitionInstances_LastCompetitionInstanceId",
                table: "Chips",
                column: "LastCompetitionInstanceId",
                principalTable: "CompetitionInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
