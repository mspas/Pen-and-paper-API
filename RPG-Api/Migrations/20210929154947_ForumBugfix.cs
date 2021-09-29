using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGApi.Migrations
{
    public partial class ForumBugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Forums_forumId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "nofparticipants",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "forumId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Forums_forumId",
                table: "Games",
                column: "forumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Forums_forumId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "forumId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "nofparticipants",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Forums_forumId",
                table: "Games",
                column: "forumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
