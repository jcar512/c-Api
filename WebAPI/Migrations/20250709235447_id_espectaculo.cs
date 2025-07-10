using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class id_espectaculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Espectaculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Espectaculos_UsuarioId",
                table: "Espectaculos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Espectaculos_Usuarios_UsuarioId",
                table: "Espectaculos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Espectaculos_Usuarios_UsuarioId",
                table: "Espectaculos");

            migrationBuilder.DropIndex(
                name: "IX_Espectaculos_UsuarioId",
                table: "Espectaculos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Espectaculos");
        }
    }
}
