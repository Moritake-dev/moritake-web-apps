using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MGAuthentication.Migrations
{
    public partial class userlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CurrentLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrentLocationName = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApprovedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCurrentLocation",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    CurrentLocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCurrentLocation", x => new { x.UserId, x.CurrentLocationId });
                    table.ForeignKey(
                        name: "FK_UserCurrentLocation_CurrentLocations_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "CurrentLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCurrentLocation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCurrentLocation_CurrentLocationId",
                table: "UserCurrentLocation",
                column: "CurrentLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCurrentLocation");

            migrationBuilder.DropTable(
                name: "CurrentLocations");

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}