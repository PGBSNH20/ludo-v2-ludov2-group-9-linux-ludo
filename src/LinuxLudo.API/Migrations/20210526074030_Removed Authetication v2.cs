using Microsoft.EntityFrameworkCore.Migrations;

namespace LinuxLudo.API.Migrations
{
    public partial class RemovedAutheticationv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStats_Games_GameId",
                table: "PlayerStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerStats",
                table: "PlayerStats");

            migrationBuilder.RenameTable(
                name: "PlayerStats",
                newName: "GameResult");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerStats_GameId",
                table: "GameResult",
                newName: "IX_GameResult_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameResult",
                table: "GameResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameResult_Games_GameId",
                table: "GameResult",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameResult_Games_GameId",
                table: "GameResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameResult",
                table: "GameResult");

            migrationBuilder.RenameTable(
                name: "GameResult",
                newName: "PlayerStats");

            migrationBuilder.RenameIndex(
                name: "IX_GameResult_GameId",
                table: "PlayerStats",
                newName: "IX_PlayerStats_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerStats",
                table: "PlayerStats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStats_Games_GameId",
                table: "PlayerStats",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
