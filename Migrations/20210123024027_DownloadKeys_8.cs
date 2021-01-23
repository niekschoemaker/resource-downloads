using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceDownloads.Migrations
{
    public partial class DownloadKeys_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DownloadKeys_Files_FileId",
                table: "DownloadKeys");

            migrationBuilder.DropIndex(
                name: "IX_DownloadKeys_FileId",
                table: "DownloadKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "DownloadKeys");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Resources");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "DownloadKeys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Resources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "DownloadKeys",
                keyColumn: "Id",
                keyValue: 1,
                column: "ResourceId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_DownloadKeys_ResourceId",
                table: "DownloadKeys",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadKeys_Resources_ResourceId",
                table: "DownloadKeys",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DownloadKeys_Resources_ResourceId",
                table: "DownloadKeys");

            migrationBuilder.DropIndex(
                name: "IX_DownloadKeys_ResourceId",
                table: "DownloadKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "DownloadKeys");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Resources");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Files");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "DownloadKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "DownloadKeys",
                keyColumn: "Id",
                keyValue: 1,
                column: "FileId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_DownloadKeys_FileId",
                table: "DownloadKeys",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadKeys_Files_FileId",
                table: "DownloadKeys",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
