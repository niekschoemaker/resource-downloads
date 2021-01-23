using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceDownloads.Migrations
{
    public partial class Resources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LatestVersion",
                table: "Resources",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "Resources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestVersion",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "Resources");
        }
    }
}
