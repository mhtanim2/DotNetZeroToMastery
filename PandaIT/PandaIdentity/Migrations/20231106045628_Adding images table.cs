using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PandaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class Addingimagestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MySubTasks_priorities_PriorityId",
                table: "MySubTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_priorities",
                table: "priorities");

            migrationBuilder.RenameTable(
                name: "priorities",
                newName: "Priorities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Priorities",
                table: "Priorities",
                column: "PriorityId");

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MySubTasks_Priorities_PriorityId",
                table: "MySubTasks",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MySubTasks_Priorities_PriorityId",
                table: "MySubTasks");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Priorities",
                table: "Priorities");

            migrationBuilder.RenameTable(
                name: "Priorities",
                newName: "priorities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_priorities",
                table: "priorities",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MySubTasks_priorities_PriorityId",
                table: "MySubTasks",
                column: "PriorityId",
                principalTable: "priorities",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
