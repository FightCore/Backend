using Microsoft.EntityFrameworkCore.Migrations;

namespace FightCore.Data.Migrations
{
    public partial class AddedDescriptionAndIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IconId",
                table: "Game",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_IconId",
                table: "Game",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_FightCoreImage_IconId",
                table: "Game",
                column: "IconId",
                principalTable: "FightCoreImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_FightCoreImage_IconId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_IconId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Game");
        }
    }
}
