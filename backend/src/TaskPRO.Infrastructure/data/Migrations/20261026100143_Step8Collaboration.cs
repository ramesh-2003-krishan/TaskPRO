using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskPRO.Infrastructure.data.Migrations
{
    /// <inheritdoc />
    public partial class Step8Collaboration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId1",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTask_Tasks_TaskItemId",
                table: "SubTask");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTask",
                table: "SubTask");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "SubTask",
                newName: "SubTasks");

            migrationBuilder.RenameIndex(
                name: "IX_SubTask_TaskItemId",
                table: "SubTasks",
                newName: "IX_SubTasks_TaskItemId");

            migrationBuilder.Sql(@"
                ALTER TABLE ""TaskComments"" DROP COLUMN ""Id"";
                ALTER TABLE ""TaskComments"" ADD COLUMN ""Id"" uuid NOT NULL DEFAULT gen_random_uuid();
                ALTER TABLE ""TaskComments"" ADD CONSTRAINT ""PK_TaskComments"" PRIMARY KEY (""Id"");
                ALTER TABLE ""TaskComments"" ALTER COLUMN ""Id"" DROP DEFAULT;
              ");
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TaskComments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "TaskAttachments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SizeBytes",
                table: "TaskAttachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "UploadedByUserId",
                table: "TaskAttachments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Notifications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskItemId",
                table: "Notifications",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "ActivityLogs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "ActivityLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "ActivityLogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SubTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTasks",
                table: "SubTasks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_UploadedByUserId",
                table: "TaskAttachments",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_ProjectId_UserId",
                table: "ProjectMembers",
                columns: new[] { "ProjectId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId_Entity_EntityId_Action_ProjectId",
                table: "ActivityLogs",
                columns: new[] { "UserId", "Entity", "EntityId", "Action", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Tasks_TaskItemId",
                table: "SubTasks",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_Users_UploadedByUserId",
                table: "TaskAttachments",
                column: "UploadedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Tasks_TaskItemId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_Users_UploadedByUserId",
                table: "TaskAttachments");

            migrationBuilder.DropIndex(
                name: "IX_TaskAttachments_UploadedByUserId",
                table: "TaskAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_ProjectId_UserId",
                table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId_Entity_EntityId_Action_ProjectId",
                table: "ActivityLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTasks",
                table: "SubTasks");

           migrationBuilder.Sql(@"
ALTER TABLE ""TaskComments"" DROP COLUMN ""Id"";
ALTER TABLE ""TaskComments"" ADD COLUMN ""Id"" integer GENERATED BY DEFAULT AS IDENTITY;
ALTER TABLE ""TaskComments"" ADD CONSTRAINT ""PK_TaskComments"" PRIMARY KEY (""Id"");
");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "TaskAttachments");

            migrationBuilder.DropColumn(
                name: "SizeBytes",
                table: "TaskAttachments");

            migrationBuilder.DropColumn(
                name: "UploadedByUserId",
                table: "TaskAttachments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SubTasks");

            migrationBuilder.RenameTable(
                name: "SubTasks",
                newName: "SubTask");

            migrationBuilder.RenameIndex(
                name: "IX_SubTasks_TaskItemId",
                table: "SubTask",
                newName: "IX_SubTask_TaskItemId");

           migrationBuilder.Sql(@"
ALTER TABLE ""TaskComments"" DROP CONSTRAINT ""PK_TaskComments"";
ALTER TABLE ""TaskComments"" DROP COLUMN ""Id"";
ALTER TABLE ""TaskComments"" ADD COLUMN ""Id"" integer GENERATED BY DEFAULT AS IDENTITY;
ALTER TABLE ""TaskComments"" ADD CONSTRAINT ""PK_TaskComments"" PRIMARY KEY (""Id"");
");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "ActivityLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTask",
                table: "SubTask",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId1",
                table: "Projects",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId1",
                table: "Projects",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTask_Tasks_TaskItemId",
                table: "SubTask",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
