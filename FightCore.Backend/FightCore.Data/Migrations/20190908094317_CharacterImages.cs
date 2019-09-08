using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FightCore.Data.Migrations
{
    public partial class CharacterImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CharacterImageId",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StockIconId",
                table: "Character",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FightCoreImage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightCoreImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_CharacterImageId",
                table: "Character",
                column: "CharacterImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_StockIconId",
                table: "Character",
                column: "StockIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_FightCoreImage_CharacterImageId",
                table: "Character",
                column: "CharacterImageId",
                principalTable: "FightCoreImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_FightCoreImage_StockIconId",
                table: "Character",
                column: "StockIconId",
                principalTable: "FightCoreImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_FightCoreImage_CharacterImageId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_FightCoreImage_StockIconId",
                table: "Character");

            migrationBuilder.DropTable(
                name: "FightCoreImage");

            migrationBuilder.DropIndex(
                name: "IX_Character_CharacterImageId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_StockIconId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CharacterImageId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "StockIconId",
                table: "Character");
        }
    }
}
