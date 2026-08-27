using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPRO.Infrastructure.Data.Migrations
{
    
    public partial class C : Migration
    {
        
       protected override void Up(MigrationBuilder migrationBuilder)
{
  
    migrationBuilder.DropForeignKey(
        name: "FK_ProjectMembers_Projects_ProjectId1",
        table: "ProjectMembers");

    migrationBuilder.DropForeignKey(
        name: "FK_ProjectMembers_Users_UserId1",
        table: "ProjectMembers");

    migrationBuilder.DropForeignKey(
        name: "FK_Projects_Users_UserId1",
        table: "Projects");

    migrationBuilder.DropForeignKey(
        name: "FK_RefreshTokens_Users_UserId1",
        table: "RefreshTokens");

    migrationBuilder.DropIndex(
        name: "IX_RefreshTokens_UserId1",
        table: "RefreshTokens");

    migrationBuilder.DropIndex(
        name: "IX_Projects_UserId1",
        table: "Projects");

    migrationBuilder.DropIndex(
        name: "IX_ProjectMembers_ProjectId1",
        table: "ProjectMembers");

    migrationBuilder.DropIndex(
        name: "IX_ProjectMembers_UserId1",
        table: "ProjectMembers");

    migrationBuilder.DropColumn(
        name: "UserId1",
        table: "RefreshTokens");

    migrationBuilder.DropColumn(
        name: "UserId1",
        table: "Projects");

    migrationBuilder.DropColumn(
        name: "ProjectId1",
        table: "ProjectMembers");

    migrationBuilder.DropColumn(
        name: "UserId1",
        table: "ProjectMembers");

  
    migrationBuilder.Sql("DELETE FROM \"RefreshTokens\";");
    migrationBuilder.Sql("DELETE FROM \"ProjectMembers\";");
    migrationBuilder.Sql("DELETE FROM \"Projects\";");

  
    migrationBuilder.AddColumn<bool>(
        name: "IsActive",
        table: "Users",
        type: "boolean",
        nullable: false,
        defaultValue: false);

    migrationBuilder.AddColumn<string>(
        name: "PasswordHashedValue",
        table: "Users",
        type: "text",
        nullable: false,
        defaultValue: "");

    migrationBuilder.AddColumn<string>(
        name: "UserEmail",
        table: "Users",
        type: "text",
        nullable: false,
        defaultValue: "");

    migrationBuilder.AddColumn<string>(
        name: "Username",
        table: "Users",
        type: "text",
        nullable: false,
        defaultValue: "");

    migrationBuilder.AddColumn<string>(
        name: "ReplacedByToken",
        table: "RefreshTokens",
        type: "text",
        nullable: true);

   
    migrationBuilder.Sql("ALTER TABLE \"RefreshTokens\" ALTER COLUMN \"UserId\" TYPE uuid USING NULL;");
    migrationBuilder.Sql("ALTER TABLE \"Projects\" ALTER COLUMN \"UserId\" TYPE uuid USING NULL;");
    migrationBuilder.Sql("ALTER TABLE \"ProjectMembers\" ALTER COLUMN \"UserId\" TYPE uuid USING NULL;");
    migrationBuilder.Sql("ALTER TABLE \"ProjectMembers\" ALTER COLUMN \"ProjectId\" TYPE uuid USING NULL;");
    migrationBuilder.Sql("ALTER TABLE \"ProjectMembers\" ALTER COLUMN \"Role\" TYPE integer USING 0;"); // 👈 Explicit conversion to integer


    migrationBuilder.CreateIndex(
        name: "IX_RefreshTokens_UserId",
        table: "RefreshTokens",
        column: "UserId");

    migrationBuilder.CreateIndex(
        name: "IX_Projects_UserId",
        table: "Projects",
        column: "UserId");

    migrationBuilder.CreateIndex(
        name: "IX_ProjectMembers_ProjectId",
        table: "ProjectMembers",
        column: "ProjectId");

    migrationBuilder.CreateIndex(
        name: "IX_ProjectMembers_UserId",
        table: "ProjectMembers",
        column: "UserId");

   
    migrationBuilder.AddForeignKey(
        name: "FK_ProjectMembers_Projects_ProjectId",
        table: "ProjectMembers",
        column: "ProjectId",
        principalTable: "Projects",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);

    migrationBuilder.AddForeignKey(
        name: "FK_ProjectMembers_Users_UserId",
        table: "ProjectMembers",
        column: "UserId",
        principalTable: "Users",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);

    migrationBuilder.AddForeignKey(
        name: "FK_Projects_Users_UserId",
        table: "Projects",
        column: "UserId",
        principalTable: "Users",
        principalColumn: "Id",
        onDelete: ReferentialAction.Restrict);

    migrationBuilder.AddForeignKey(
        name: "FK_RefreshTokens_Users_UserId",
        table: "RefreshTokens",
        column: "UserId",
        principalTable: "Users",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
}
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Users_UserId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_UserId",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHashedValue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReplacedByToken",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "ProjectMembers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}