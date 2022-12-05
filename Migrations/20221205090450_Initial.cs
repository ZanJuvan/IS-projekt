using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeOrganizer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uporabnik",
                columns: table => new
                {
                    UporabnikID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lokacija = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uporabnik", x => x.UporabnikID);
                });

            migrationBuilder.CreateTable(
                name: "Cebeljnjak",
                columns: table => new
                {
                    CebeljnjakID = table.Column<int>(type: "int", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lokacija = table.Column<int>(type: "int", nullable: false),
                    UporabnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cebeljnjak", x => x.CebeljnjakID);
                    table.ForeignKey(
                        name: "FK_Cebeljnjak_Uporabnik_UporabnikID",
                        column: x => x.UporabnikID,
                        principalTable: "Uporabnik",
                        principalColumn: "UporabnikID");
                });

            migrationBuilder.CreateTable(
                name: "Odhodek",
                columns: table => new
                {
                    OdhodekID = table.Column<int>(type: "int", nullable: false),
                    Vrsta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Količina = table.Column<int>(type: "int", nullable: false),
                    Vrednost = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UporabnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odhodek", x => x.OdhodekID);
                    table.ForeignKey(
                        name: "FK_Odhodek_Uporabnik_UporabnikID",
                        column: x => x.UporabnikID,
                        principalTable: "Uporabnik",
                        principalColumn: "UporabnikID");
                });

            migrationBuilder.CreateTable(
                name: "Prihodek",
                columns: table => new
                {
                    PrihodekID = table.Column<int>(type: "int", nullable: false),
                    Vrsta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Količina = table.Column<int>(type: "int", nullable: false),
                    Vrednost = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UporabnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prihodek", x => x.PrihodekID);
                    table.ForeignKey(
                        name: "FK_Prihodek_Uporabnik_UporabnikID",
                        column: x => x.UporabnikID,
                        principalTable: "Uporabnik",
                        principalColumn: "UporabnikID");
                });

            migrationBuilder.CreateTable(
                name: "Panj",
                columns: table => new
                {
                    PanjID = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CebeljnjakID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panj", x => x.PanjID);
                    table.ForeignKey(
                        name: "FK_Panj_Cebeljnjak_CebeljnjakID",
                        column: x => x.CebeljnjakID,
                        principalTable: "Cebeljnjak",
                        principalColumn: "CebeljnjakID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cebeljnjak_UporabnikID",
                table: "Cebeljnjak",
                column: "UporabnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Odhodek_UporabnikID",
                table: "Odhodek",
                column: "UporabnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Panj_CebeljnjakID",
                table: "Panj",
                column: "CebeljnjakID");

            migrationBuilder.CreateIndex(
                name: "IX_Prihodek_UporabnikID",
                table: "Prihodek",
                column: "UporabnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odhodek");

            migrationBuilder.DropTable(
                name: "Panj");

            migrationBuilder.DropTable(
                name: "Prihodek");

            migrationBuilder.DropTable(
                name: "Cebeljnjak");

            migrationBuilder.DropTable(
                name: "Uporabnik");
        }
    }
}
