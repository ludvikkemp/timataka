using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class UsersInClubs_table_added_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersInClubs",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SportId = table.Column<int>(nullable: false),
                    ClubId = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInClubs", x => new { x.UserId, x.SportId });
                    table.UniqueConstraint("AK_UsersInClubs_SportId_UserId", x => new { x.SportId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersInClubs_Sports_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UsersInClubs_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UsersInClubs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInClubs_ClubId",
                table: "UsersInClubs",
                column: "ClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInClubs");
        }
    }
}
