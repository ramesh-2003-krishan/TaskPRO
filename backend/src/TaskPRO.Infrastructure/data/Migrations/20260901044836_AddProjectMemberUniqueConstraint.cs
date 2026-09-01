using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPRO.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectMemberUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId1",
                table: "Projects",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId1",
                table: "Projects",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Projects");
        }
    }
}
