using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class ManagesCompetitions_KEY_added_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ManagesCompetitions_CompetitionId",
                table: "ManagesCompetitions");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ManagesCompetitions_CompetitionId_UserId",
                table: "ManagesCompetitions",
                columns: new[] { "CompetitionId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ManagesCompetitions_CompetitionId_UserId",
                table: "ManagesCompetitions");

            migrationBuilder.CreateIndex(
                name: "IX_ManagesCompetitions_CompetitionId",
                table: "ManagesCompetitions",
                column: "CompetitionId");
        }
    }
}
