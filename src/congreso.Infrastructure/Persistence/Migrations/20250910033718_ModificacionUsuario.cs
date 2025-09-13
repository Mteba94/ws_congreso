using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congreso.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_NivelesAcademicos_nivelAcademicoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TiposParticipante_tipoParticipante",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "tipoParticipante",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "nivelAcademicoId",
                table: "Usuarios",
                type: "int",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_NivelesAcademicos_nivelAcademicoId",
                table: "Usuarios",
                column: "nivelAcademicoId",
                principalTable: "NivelesAcademicos",
                principalColumn: "nivelAcademicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TiposParticipante_tipoParticipante",
                table: "Usuarios",
                column: "tipoParticipante",
                principalTable: "TiposParticipante",
                principalColumn: "idTipoParticipante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_NivelesAcademicos_nivelAcademicoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TiposParticipante_tipoParticipante",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "tipoParticipante",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "nivelAcademicoId",
                table: "Usuarios",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_NivelesAcademicos_nivelAcademicoId",
                table: "Usuarios",
                column: "nivelAcademicoId",
                principalTable: "NivelesAcademicos",
                principalColumn: "nivelAcademicoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TiposParticipante_tipoParticipante",
                table: "Usuarios",
                column: "tipoParticipante",
                principalTable: "TiposParticipante",
                principalColumn: "idTipoParticipante",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
