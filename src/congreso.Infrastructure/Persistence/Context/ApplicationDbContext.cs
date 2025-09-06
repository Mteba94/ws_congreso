using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace congreso.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
    {
        public DbSet<Congreso> Congresos { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<TipoActividad> TiposActividades { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ponente> Ponentes { get; set; }
        public DbSet<PonenteTag> PonenteTags { get; set; }
        public DbSet<ActividadPonente> ActividadesPonentes { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUsuario> RolesUsuarios { get; set; }
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<TipoParticipante> TiposParticipante { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<NivelAcademico> NivelesAcademicos { get; set; }
        public DbSet<CodigoVerificacion> CodigosVerificacion { get; set; }
        public DbSet<NivelDificultad> NivelesDificultad { get; set; }
        public DbSet<ObjetivoActividad> ObjetivosActividad { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
