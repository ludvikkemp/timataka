using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Timataka.Core.Data.Migrations
{
    public partial class Changing_Markers_table_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Events_EventId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Heats_HeatId",
                table: "Markers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markers",
                table: "Markers");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Markers",
                newName: "CompetitionInstanceId");

            migrationBuilder.AlterColumn<int>(
                name: "HeatId",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Markers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markers",
                table: "Markers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_CompetitionInstanceId",
                table: "Markers",
                column: "CompetitionInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_CompetitionInstances_CompetitionInstanceId",
                table: "Markers",
                column: "CompetitionInstanceId",
                principalTable: "CompetitionInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Heats_HeatId",
                table: "Markers",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_CompetitionInstances_CompetitionInstanceId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Heats_HeatId",
                table: "Markers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markers",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Markers_CompetitionInstanceId",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Markers");

            migrationBuilder.RenameColumn(
                name: "CompetitionInstanceId",
                table: "Markers",
                newName: "EventId");

            migrationBuilder.AlterColumn<int>(
                name: "HeatId",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markers",
                table: "Markers",
                columns: new[] { "EventId", "HeatId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Events_EventId",
                table: "Markers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Heats_HeatId",
                table: "Markers",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
