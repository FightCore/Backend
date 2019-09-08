using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FightCore.Data.Migrations
{
    public partial class GameSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SeriesId",
                table: "Character",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameSeries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    GameIconId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSeries_FightCoreImage_GameIconId",
                        column: x => x.GameIconId,
                        principalTable: "FightCoreImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_SeriesId",
                table: "Character",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSeries_GameIconId",
                table: "GameSeries",
                column: "GameIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_GameSeries_SeriesId",
                table: "Character",
                column: "SeriesId",
                principalTable: "GameSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_GameSeries_SeriesId",
                table: "Character");

            migrationBuilder.DropTable(
                name: "GameSeries");

            migrationBuilder.DropIndex(
                name: "IX_Character_SeriesId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Character");
        }
    }
}
