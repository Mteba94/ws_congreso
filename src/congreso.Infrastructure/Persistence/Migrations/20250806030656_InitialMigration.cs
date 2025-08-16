using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congreso.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Congresos",
                columns: table => new
                {
                    idCongreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Congresos", x => x.idCongreso);
                });

            migrationBuilder.CreateTable(
                name: "NivelesAcademicos",
                columns: table => new
                {
                    nivelAcademicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesAcademicos", x => x.nivelAcademicoId);
                });

            migrationBuilder.CreateTable(
                name: "Ponentes",
                columns: table => new
                {
                    idPonente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    empresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponentes", x => x.idPonente);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    idRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.idRole);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    idTag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.idTag);
                });

            migrationBuilder.CreateTable(
                name: "TiposActividades",
                columns: table => new
                {
                    idTipoActividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposActividades", x => x.idTipoActividad);
                });

            migrationBuilder.CreateTable(
                name: "TiposIdentificacion",
                columns: table => new
                {
                    idTipoIdentificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentificacion", x => x.idTipoIdentificacion);
                });

            migrationBuilder.CreateTable(
                name: "TiposParticipante",
                columns: table => new
                {
                    idTipoParticipante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposParticipante", x => x.idTipoParticipante);
                });

            migrationBuilder.CreateTable(
                name: "PonenteTags",
                columns: table => new
                {
                    idPonenteTags = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    PonenteId = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PonenteTags", x => x.idPonenteTags);
                    table.ForeignKey(
                        name: "FK_PonenteTags_Ponentes_PonenteId",
                        column: x => x.PonenteId,
                        principalTable: "Ponentes",
                        principalColumn: "idPonente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PonenteTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "idTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    idActividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCongreso = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    idTipoActividad = table.Column<int>(type: "int", nullable: false),
                    FechaActividad = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuposDisponibles = table.Column<int>(type: "int", nullable: false),
                    CuposTotales = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RequisitosPrevios = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.idActividad);
                    table.ForeignKey(
                        name: "FK_Actividades_Congresos_idCongreso",
                        column: x => x.idCongreso,
                        principalTable: "Congresos",
                        principalColumn: "idCongreso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actividades_TiposActividades_idTipoActividad",
                        column: x => x.idTipoActividad,
                        principalTable: "TiposActividades",
                        principalColumn: "idTipoActividad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    primerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    segundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    primerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    segundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tipoParticipante = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipoIdentificacion = table.Column<int>(type: "int", nullable: false),
                    numeroIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nivelAcademicoId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    semestre = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    emailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    lockOutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lockOutEnabled = table.Column<int>(type: "int", nullable: true),
                    accessFailedCount = table.Column<int>(type: "int", nullable: true),
                    securityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_NivelesAcademicos_nivelAcademicoId",
                        column: x => x.nivelAcademicoId,
                        principalTable: "NivelesAcademicos",
                        principalColumn: "nivelAcademicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposIdentificacion_tipoIdentificacion",
                        column: x => x.tipoIdentificacion,
                        principalTable: "TiposIdentificacion",
                        principalColumn: "idTipoIdentificacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposParticipante_tipoParticipante",
                        column: x => x.tipoParticipante,
                        principalTable: "TiposParticipante",
                        principalColumn: "idTipoParticipante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActividadesPonentes",
                columns: table => new
                {
                    idActividadPonente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    PonenteId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadesPonentes", x => x.idActividadPonente);
                    table.ForeignKey(
                        name: "FK_ActividadesPonentes_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "idActividad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActividadesPonentes_Ponentes_PonenteId",
                        column: x => x.PonenteId,
                        principalTable: "Ponentes",
                        principalColumn: "idPonente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    idInscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idActividad = table.Column<int>(type: "int", nullable: false),
                    fechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.idInscripcion);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Actividades_idActividad",
                        column: x => x.idActividad,
                        principalTable: "Actividades",
                        principalColumn: "idActividad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsuarios",
                columns: table => new
                {
                    idUsuarioRoles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsuarios", x => x.idUsuarioRoles);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "idRole",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    idAsistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.idAsistencia);
                    table.ForeignKey(
                        name: "FK_Asistencias_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "idActividad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencias_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "idInscripcion");
                });

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    idDiploma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    IdTipoDiploma = table.Column<int>(type: "int", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    fechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioEliminacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.idDiploma);
                    table.ForeignKey(
                        name: "FK_Diplomas_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "idActividad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diplomas_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "idInscripcion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_idCongreso",
                table: "Actividades",
                column: "idCongreso");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_idTipoActividad",
                table: "Actividades",
                column: "idTipoActividad");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesPonentes_ActividadId",
                table: "ActividadesPonentes",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesPonentes_PonenteId",
                table: "ActividadesPonentes",
                column: "PonenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_ActividadId",
                table: "Asistencias",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_InscripcionId",
                table: "Asistencias",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_ActividadId",
                table: "Diplomas",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_InscripcionId",
                table: "Diplomas",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_idActividad",
                table: "Inscripciones",
                column: "idActividad");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_idUsuario",
                table: "Inscripciones",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PonenteTags_PonenteId",
                table: "PonenteTags",
                column: "PonenteId");

            migrationBuilder.CreateIndex(
                name: "IX_PonenteTags_TagId",
                table: "PonenteTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_RoleId",
                table: "RolesUsuarios",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_UserId",
                table: "RolesUsuarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_email",
                table: "Usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_nivelAcademicoId",
                table: "Usuarios",
                column: "nivelAcademicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_tipoIdentificacion",
                table: "Usuarios",
                column: "tipoIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_tipoParticipante",
                table: "Usuarios",
                column: "tipoParticipante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadesPonentes");

            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "PonenteTags");

            migrationBuilder.DropTable(
                name: "RolesUsuarios");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Ponentes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Congresos");

            migrationBuilder.DropTable(
                name: "TiposActividades");

            migrationBuilder.DropTable(
                name: "NivelesAcademicos");

            migrationBuilder.DropTable(
                name: "TiposIdentificacion");

            migrationBuilder.DropTable(
                name: "TiposParticipante");
        }
    }
}
