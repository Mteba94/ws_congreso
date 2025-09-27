using congreso.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace congreso.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
    {
        private readonly IConfiguration _configuration = configuration;

        public DbSet<Congreso> Congresos { get; set; } = null!;
        public DbSet<Actividad> Actividades { get; set; } = null!;
        public DbSet<TipoActividad> TiposActividades { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Ponente> Ponentes { get; set; } = null!;
        public DbSet<PonenteTag> PonenteTags { get; set; } = null!;
        public DbSet<ActividadPonente> ActividadesPonentes { get; set; } = null!;
        public DbSet<User> Usuarios { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RoleUsuario> RolesUsuarios { get; set; } = null!;
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; } = null!;
        public DbSet<TipoParticipante> TiposParticipante { get; set; } = null!;
        public DbSet<Inscripcion> Inscripciones { get; set; } = null!;
        public DbSet<Asistencia> Asistencias { get; set; } = null!;
        public DbSet<Diploma> Diplomas { get; set; } = null!;
        public DbSet<NivelAcademico> NivelesAcademicos { get; set; } = null!;
        public DbSet<CodigoVerificacion> CodigosVerificacion { get; set; } = null!;
        public DbSet<NivelDificultad> NivelesDificultad { get; set; } = null!;
        public DbSet<ObjetivoActividad> ObjetivosActividad { get; set; } = null!;
        public DbSet<School> Schools { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<MenuRole> MenusRoles { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<RolePermission> RolesPermisos { get; set; } = null!;
        public DbSet<Permission> Permisos { get; set; } = null!;
        public DbSet<MaterialActividad> MaterialesActividad { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("CongressConnection"));
    }
}
