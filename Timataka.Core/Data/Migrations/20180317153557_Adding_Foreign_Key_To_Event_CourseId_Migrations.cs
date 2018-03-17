using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Adding_Foreign_Key_To_Event_CourseId_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Event_CourseId",
                table: "Event",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Courses_CourseId",
                table: "Event",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Courses_CourseId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_CourseId",
                table: "Event");
        }
    }
}
