using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class espectaculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistasEspectaculos");

            migrationBuilder.AddColumn<int>(
                name: "ArtistaId",
                table: "Espectaculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Espectaculos_ArtistaId",
                table: "Espectaculos",
                column: "ArtistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Espectaculos_Artistas_ArtistaId",
                table: "Espectaculos",
                column: "ArtistaId",
                principalTable: "Artistas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Espectaculos_Artistas_ArtistaId",
                table: "Espectaculos");

            migrationBuilder.DropIndex(
                name: "IX_Espectaculos_ArtistaId",
                table: "Espectaculos");

            migrationBuilder.DropColumn(
                name: "ArtistaId",
                table: "Espectaculos");

            migrationBuilder.CreateTable(
                name: "ArtistasEspectaculos",
                columns: table => new
                {
                    ArtistaId = table.Column<int>(type: "int", nullable: false),
                    EspectaculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistasEspectaculos", x => new { x.ArtistaId, x.EspectaculoId });
                    table.ForeignKey(
                        name: "FK_ArtistasEspectaculos_Artistas_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistasEspectaculos_Espectaculos_EspectaculoId",
                        column: x => x.EspectaculoId,
                        principalTable: "Espectaculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistasEspectaculos_EspectaculoId",
                table: "ArtistasEspectaculos",
                column: "EspectaculoId");
        }
    }
}
