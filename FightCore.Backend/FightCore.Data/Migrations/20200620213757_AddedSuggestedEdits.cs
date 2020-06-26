using Microsoft.EntityFrameworkCore.Migrations;

namespace FightCore.Data.Migrations
{
    public partial class AddedSuggestedEdits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuggestedEdit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Editable = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    ApprovedByUserId = table.Column<long>(nullable: true),
                    Original = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestedEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestedEdit_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuggestedEdit_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedEdit_ApprovedByUserId",
                table: "SuggestedEdit",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedEdit_UserId",
                table: "SuggestedEdit",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestedEdit");
        }
    }
}
