using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Fixing_CompetitionInstance_virtual_was_discipline_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionInstances_Disciplines_CompetitionId",
                table: "CompetitionInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionInstances_Competitions_CompetitionId",
                table: "CompetitionInstances",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionInstances_Competitions_CompetitionId",
                table: "CompetitionInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionInstances_Disciplines_CompetitionId",
                table: "CompetitionInstances",
                column: "CompetitionId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
