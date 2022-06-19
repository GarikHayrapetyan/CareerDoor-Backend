using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddJobExperienceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Jobs");

            migrationBuilder.AddColumn<Guid>(
                name: "JobExperienceId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "JobExperience",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExperience", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobExperienceId",
                table: "Jobs",
                column: "JobExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobExperience_JobExperienceId",
                table: "Jobs",
                column: "JobExperienceId",
                principalTable: "JobExperience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobExperience_JobExperienceId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobExperience");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobExperienceId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobExperienceId",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
