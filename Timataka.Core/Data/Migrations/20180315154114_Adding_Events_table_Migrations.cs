using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_Events_table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveChip = table.Column<bool>(nullable: false),
                    CompetitionInstanceId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    DisciplineId = table.Column<int>(nullable: false),
                    DistanceOffset = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Laps = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Splits = table.Column<int>(nullable: false),
                    StartInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_CompetitionInstances_CompetitionInstanceId",
                        column: x => x.CompetitionInstanceId,
                        principalTable: "CompetitionInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CompetitionInstanceId",
                table: "Events",
                column: "CompetitionInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_DisciplineId",
                table: "Events",
                column: "DisciplineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
