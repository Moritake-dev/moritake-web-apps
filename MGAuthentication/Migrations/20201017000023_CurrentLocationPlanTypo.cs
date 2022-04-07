using Microsoft.EntityFrameworkCore.Migrations;

namespace MGAuthentication.Migrations
{
    public partial class CurrentLocationPlanTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLoactionPlan",
                table: "CurrentLocations");

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocationPlan",
                table: "CurrentLocations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocationPlan",
                table: "CurrentLocations");

            migrationBuilder.AddColumn<string>(
                name: "CurrentLoactionPlan",
                table: "CurrentLocations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
