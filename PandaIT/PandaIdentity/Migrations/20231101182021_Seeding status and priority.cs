using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PandaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class Seedingstatusandpriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MySubTasks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MySubTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MyTasks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MySubTasks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedTo",
                table: "MySubTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityId",
                table: "MySubTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "MySubTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "priorities",
                columns: table => new
                {
                    PriorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priorities", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "StatusType" },
                values: new object[,]
                {
                    { new Guid("57c9293e-73f9-43eb-9d8d-2dcfc0d3aa00"), "ToDo" },
                    { new Guid("bcd426c5-dd7c-48fd-bab0-673d2c18f50c"), "Done" },
                    { new Guid("be0885f8-885c-47bc-bd90-f6d9e4f7f568"), "Doing" }
                });

            migrationBuilder.InsertData(
                table: "priorities",
                columns: new[] { "PriorityId", "PriorityType" },
                values: new object[,]
                {
                    { new Guid("bcd426c5-dd7c-48fd-bab0-673d2c18f50c"), "Hard" },
                    { new Guid("d077477f-7aca-4cd0-8dd3-6f01865232c1"), "Medium" },
                    { new Guid("d9dc7ae1-5ec5-422a-a902-5fc375d29d2d"), "Easy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MySubTasks_PriorityId",
                table: "MySubTasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_MySubTasks_StatusId",
                table: "MySubTasks",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_MySubTasks_Statuses_StatusId",
                table: "MySubTasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MySubTasks_priorities_PriorityId",
                table: "MySubTasks",
                column: "PriorityId",
                principalTable: "priorities",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MySubTasks_Statuses_StatusId",
                table: "MySubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_MySubTasks_priorities_PriorityId",
                table: "MySubTasks");

            migrationBuilder.DropTable(
                name: "priorities");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_MySubTasks_PriorityId",
                table: "MySubTasks");

            migrationBuilder.DropIndex(
                name: "IX_MySubTasks_StatusId",
                table: "MySubTasks");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "MySubTasks");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "MySubTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MyTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MySubTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedTo",
                table: "MySubTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "MySubTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MySubTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
