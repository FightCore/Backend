using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FightCore.Data.Migrations
{
    public partial class CharacterVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoResource",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    YoutubeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterVideo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VideoId = table.Column<long>(nullable: true),
                    CharacterId = table.Column<long>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterVideo");

            migrationBuilder.DropTable(
                name: "VideoResource");
        }
    }
}
