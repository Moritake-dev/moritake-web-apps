using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MGAuthentication.Migrations
{
    public partial class usercurrentlocation_isapproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "CurrentLocations");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "CurrentLocations");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "UserCurrentLocation",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "UserCurrentLocation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "CurrentLocations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "UserCurrentLocation");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "UserCurrentLocation");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "CurrentLocations");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "CurrentLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "CurrentLocations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}