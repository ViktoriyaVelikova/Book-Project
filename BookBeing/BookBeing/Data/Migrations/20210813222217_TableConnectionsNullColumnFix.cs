using Microsoft.EntityFrameworkCore.Migrations;

namespace BookBeing.Data.Migrations
{
    public partial class TableConnectionsNullColumnFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Libraries_LibraryId1",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_AspNetUsers_UserId1",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_UserId1",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_LibraryId1",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "LibraryId1",
                table: "Announcements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Libraries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LibraryId1",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId1",
                table: "Libraries",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_LibraryId1",
                table: "Announcements",
                column: "LibraryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Libraries_LibraryId1",
                table: "Announcements",
                column: "LibraryId1",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_AspNetUsers_UserId1",
                table: "Libraries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
