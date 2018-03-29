using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_Chip_Table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chips",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    LastCompetitionInstanceId = table.Column<int>(nullable: false),
                    LastSeen = table.Column<DateTime>(nullable: false),
                    LastUserId = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chips", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Chips_CompetitionInstances_LastCompetitionInstanceId",
                        column: x => x.LastCompetitionInstanceId,
                        principalTable: "CompetitionInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Chips_AspNetUsers_LastUserId",
                        column: x => x.LastUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chips_LastCompetitionInstanceId",
                table: "Chips",
                column: "LastCompetitionInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Chips_LastUserId",
                table: "Chips",
                column: "LastUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chips");
        }
    }
}
