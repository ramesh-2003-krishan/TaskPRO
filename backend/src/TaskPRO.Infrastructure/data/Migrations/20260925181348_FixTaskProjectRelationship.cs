using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPRO.Infrastructure.data.Migrations
{
    /// <inheritdoc />
    public partial class FixTaskProjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_projectId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Tasks",
                newName: "LegacyProjectId");
            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_projectId",
                table: "Tasks",
                column: "projectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_projectId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "LegacyProjectId",
                table: "Tasks",
                newName: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_projectId",
                table: "Tasks",
                column: "projectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
