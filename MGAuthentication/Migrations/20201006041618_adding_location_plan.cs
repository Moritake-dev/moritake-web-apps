using Microsoft.EntityFrameworkCore.Migrations;

namespace MGAuthentication.Migrations
{
    public partial class adding_location_plan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentLoactionPlan",
                table: "CurrentLocations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLoactionPlan",
                table: "CurrentLocations");
        }
    }
}
