using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceDownloads.Migrations
{
    public partial class DownloadKeys_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DownloadKeys_Files_FileId",
                table: "DownloadKeys");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "DownloadKeys",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Path" },
                values: new object[] { 1, "/tmp/test.txt" });

            migrationBuilder.InsertData(
                table: "DownloadKeys",
                columns: new[] { "Id", "FileId", "Key" },
                values: new object[] { 1, 1, "test" });

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadKeys_Files_FileId",
                table: "DownloadKeys",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DownloadKeys_Files_FileId",
                table: "DownloadKeys");

            migrationBuilder.DeleteData(
                table: "DownloadKeys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "DownloadKeys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadKeys_Files_FileId",
                table: "DownloadKeys",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
