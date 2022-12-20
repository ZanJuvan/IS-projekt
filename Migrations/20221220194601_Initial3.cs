using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeOrganizer.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MenjavaMatice",
                table: "Evidenca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "donosMedu",
                table: "Evidenca",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenjavaMatice",
                table: "Evidenca");

            migrationBuilder.DropColumn(
                name: "donosMedu",
                table: "Evidenca");
        }
    }
}
