using Microsoft.EntityFrameworkCore.Migrations;

namespace LinuxLudo.API.Migrations
{
    public partial class RemvedPKfromPivottabletoreplacewithancompositionkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GamePlayerPivot_GameId",
                table: "GamePlayerPivot");

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_GameId_PlayerId",
                table: "GamePlayerPivot",
                columns: new[] { "GameId", "PlayerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_GameId_PlayerId",
                table: "GamePlayerPivot");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerPivot_GameId",
                table: "GamePlayerPivot",
                column: "GameId");
        }
    }
}
