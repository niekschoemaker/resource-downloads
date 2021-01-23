using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceDownloads.Migrations
{
    public partial class DownloadKeys_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "DownloadKeys",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DownloadKeys_Key",
                table: "DownloadKeys",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DownloadKeys_Key",
                table: "DownloadKeys");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "DownloadKeys",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
