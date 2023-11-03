using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PandaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class paandaIdentityKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyTasks",
                columns: table => new
                {
                    TaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTasks", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "MySubTasks",
                columns: table => new
                {
                    SubTaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MyTaskTaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MySubTasks", x => x.SubTaskID);
                    table.ForeignKey(
                        name: "FK_MySubTasks_MyTasks_MyTaskTaskID",
                        column: x => x.MyTaskTaskID,
                        principalTable: "MyTasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MySubTasks_MyTaskTaskID",
                table: "MySubTasks",
                column: "MyTaskTaskID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MySubTasks");

            migrationBuilder.DropTable(
                name: "MyTasks");
        }
    }
}
