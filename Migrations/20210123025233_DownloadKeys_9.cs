using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceDownloads.Migrations
{
    public partial class DownloadKeys_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Resources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Resources");
        }
    }
}
