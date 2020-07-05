using Microsoft.EntityFrameworkCore.Migrations;

namespace FightCore.Data.Migrations
{
    public partial class RemovedCharacterVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterVideo");

            migrationBuilder.AddColumn<long>(
                name: "CharacterId",
                table: "VideoResource",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoResource_CharacterId",
                table: "VideoResource",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoResource_Character_CharacterId",
                table: "VideoResource",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoResource_Character_CharacterId",
                table: "VideoResource");

            migrationBuilder.DropIndex(
                name: "IX_VideoResource_CharacterId",
                table: "VideoResource");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "VideoResource");

            migrationBuilder.CreateTable(
                name: "CharacterVideo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<long>(type: "bigint", nullable: true),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterVideo_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterVideo_VideoResource_VideoId",
                        column: x => x.VideoId,
                        principalTable: "VideoResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterVideo_CharacterId",
                table: "CharacterVideo",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterVideo_VideoId",
                table: "CharacterVideo",
                column: "VideoId");
        }
    }
}
