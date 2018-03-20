using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Changing_Event_table_to_Events_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_CompetitionInstances_CompetitionInstanceId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Courses_CourseId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Disciplines_DisciplineId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Heats_Event_EventId",
                table: "Heats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Event_DisciplineId",
                table: "Events",
                newName: "IX_Events_DisciplineId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_CourseId",
                table: "Events",
                newName: "IX_Events_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_CompetitionInstanceId",
                table: "Events",
                newName: "IX_Events_CompetitionInstanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CompetitionInstances_CompetitionInstanceId",
                table: "Events",
                column: "CompetitionInstanceId",
                principalTable: "CompetitionInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Courses_CourseId",
                table: "Events",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Disciplines_DisciplineId",
                table: "Events",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Heats_Events_EventId",
                table: "Heats",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_CompetitionInstances_CompetitionInstanceId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Courses_CourseId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Disciplines_DisciplineId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Heats_Events_EventId",
                table: "Heats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Events_DisciplineId",
                table: "Event",
                newName: "IX_Event_DisciplineId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CourseId",
                table: "Event",
                newName: "IX_Event_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CompetitionInstanceId",
                table: "Event",
                newName: "IX_Event_CompetitionInstanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_CompetitionInstances_CompetitionInstanceId",
                table: "Event",
                column: "CompetitionInstanceId",
                principalTable: "CompetitionInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Courses_CourseId",
                table: "Event",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Disciplines_DisciplineId",
                table: "Event",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Heats_Event_EventId",
                table: "Heats",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
