using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congreso.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InscripcionResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsGanador",
                table: "Inscripciones",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Puntaje",
                table: "Inscripciones",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsGanador",
                table: "Inscripciones");

            migrationBuilder.DropColumn(
                name: "Puntaje",
                table: "Inscripciones");
        }
    }
}
