using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congreso.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImagenActividad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estado",
                table: "PonenteTags",
                newName: "Estado");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Actividades",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Actividades");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "PonenteTags",
                newName: "estado");
        }
    }
}
