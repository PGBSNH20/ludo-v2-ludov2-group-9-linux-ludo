using Microsoft.EntityFrameworkCore.Migrations;

namespace LinuxLudo.API.Migrations
{
    public partial class MapGamestatetoenumstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "Waiting",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Waiting");
        }
    }
}
