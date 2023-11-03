using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PandaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class addedfieldonsubtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "MySubTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "MySubTasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MySubTasks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MySubTasks");
        }
    }
}
