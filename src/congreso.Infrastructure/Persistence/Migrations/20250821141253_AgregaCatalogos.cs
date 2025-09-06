using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congreso.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregaCatalogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "purpose",
                table: "CodigosVerificacion",
                newName: "Purpose");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NivelDificultadId",
                table: "Actividades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NivelesDificultad",
                columns: table => new
                {
                    idDificultad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesDificultad", x => x.idDificultad);
                });

            migrationBuilder.CreateTable(
                name: "ObjetivosActividad",
                columns: table => new
                {
                    idObjetivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    ObjetivoDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetivosActividad", x => x.idObjetivo);
                    table.ForeignKey(
                        name: "FK_ObjetivosActividad_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "idActividad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    idSchool = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.idSchool);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SchoolId",
                table: "Usuarios",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_NivelDificultadId",
                table: "Actividades",
                column: "NivelDificultadId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivosActividad_ActividadId",
                table: "ObjetivosActividad",
                column: "ActividadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_NivelesDificultad_NivelDificultadId",
                table: "Actividades",
                column: "NivelDificultadId",
                principalTable: "NivelesDificultad",
                principalColumn: "idDificultad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Schools_SchoolId",
                table: "Usuarios",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "idSchool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_NivelesDificultad_NivelDificultadId",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Schools_SchoolId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "NivelesDificultad");

            migrationBuilder.DropTable(
                name: "ObjetivosActividad");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SchoolId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Actividades_NivelDificultadId",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NivelDificultadId",
                table: "Actividades");

            migrationBuilder.RenameColumn(
                name: "Purpose",
                table: "CodigosVerificacion",
                newName: "purpose");
        }
    }
}
