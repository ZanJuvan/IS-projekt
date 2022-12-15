using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeOrganizer.Migrations
{
    public partial class ExtendCebelj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UporabnikId",
                table: "Cebeljnjak",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cebeljnjak_UporabnikId",
                table: "Cebeljnjak",
                column: "UporabnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cebeljnjak_AspNetUsers_UporabnikId",
                table: "Cebeljnjak",
                column: "UporabnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cebeljnjak_AspNetUsers_UporabnikId",
                table: "Cebeljnjak");

            migrationBuilder.DropIndex(
                name: "IX_Cebeljnjak_UporabnikId",
                table: "Cebeljnjak");

            migrationBuilder.DropColumn(
                name: "UporabnikId",
                table: "Cebeljnjak");
        }
    }
}
