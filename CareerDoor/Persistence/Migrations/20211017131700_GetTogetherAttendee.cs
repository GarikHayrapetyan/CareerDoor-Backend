using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class GetTogetherAttendee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetTogetherAttendees",
                columns: table => new
                {
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GetTogetherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHost = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetTogetherAttendees", x => new { x.AppUserID, x.GetTogetherId });
                    table.ForeignKey(
                        name: "FK_GetTogetherAttendees_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetTogetherAttendees_GetTogethers_GetTogetherId",
                        column: x => x.GetTogetherId,
                        principalTable: "GetTogethers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GetTogetherAttendees_GetTogetherId",
                table: "GetTogetherAttendees",
                column: "GetTogetherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetTogetherAttendees");
        }
    }
}
