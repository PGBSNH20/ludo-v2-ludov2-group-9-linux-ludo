using Microsoft.EntityFrameworkCore.Migrations;

namespace LinuxLudo.API.Migrations
{
    public partial class UpdateGameState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
