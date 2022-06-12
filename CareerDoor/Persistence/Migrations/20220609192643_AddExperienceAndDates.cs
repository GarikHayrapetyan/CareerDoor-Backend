using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Persistence.Migrations
{
    public partial class AddExperienceAndDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Jobs",
                newName: "Creation");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Jobs",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "Jobs",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creation",
                table: "Jobs",
                newName: "Date");

            migrationBuilder.DropColumn(
              name: "Experience",
              table: "Jobs");

            migrationBuilder.DropColumn(
              name: "Expiration",
              table: "Jobs");

        }
    }
}
