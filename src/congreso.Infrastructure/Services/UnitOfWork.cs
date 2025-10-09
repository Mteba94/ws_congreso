using congreso.Application.Interfaces.ExternalWS;
using congreso.Application.Interfaces.Persistence;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace congreso.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Congreso> Congreso { get; }
        public IUserRepository User { get; }
        public ITipoIdentificacionRepository TipoIdentificacion { get; }
        public ITipoParticipanteRepository TipoParticipante { get; }
        public ICodigoRepository CodigoVerificacion { get; }
        public ICommonRepository<NivelAcademico> NivelAcademico { get; }
        public ICommonRepository<School> School { get; }
        public IActividadRepository Actividad { get; }
        public ICommonRepository<TipoActividad> TipoActividad { get; }
        public ICommonRepository<RoleUsuario> RoleUsuario { get; }
        public ICommonRepository<Role> Role { get; }
        public IPermisosRepository Permisos { get; }
        public IMenuRepository Menus { get; }
        public IRefreshTokenRepository RefreshToken { get; }
        public ICommonRepository<NivelDificultad> NivelDificultad { get; }
        public ICommonRepository<Tag> Tag { get; }
        public IPonenteTagRepository PonenteTag { get; }
        public IPonenteRepository Ponente { get; }
        public IObjetivoActividadRepository ObjetivoActividad { get; }
        public ICommonRepository<ActividadPonente> ActividadPonente { get; }
        public IMaterialActividadRepository MaterialActividad { get; }
        public IAzureStorage azureStorage { get; }
        public IInscripcionRepository Inscripcion { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IGenericRepository<Congreso> CongresoRepository,
            IUserRepository user,
            ITipoIdentificacionRepository tipoIdentificacionRepository,
            ITipoParticipanteRepository tipoParticipante,
            ICodigoRepository codigoVerificacion,
            ICommonRepository<NivelAcademico> nivelAcademico,
            ICommonRepository<School> school,
            IActividadRepository actividad,
            ICommonRepository<TipoActividad> tipoActividad,
            ICommonRepository<RoleUsuario> roleUsuario,
            ICommonRepository<Role> role,
            IPermisosRepository permisos,
            IMenuRepository menus,
            IRefreshTokenRepository refreshToken,
            ICommonRepository<NivelDificultad> nivelDificultad,
            ICommonRepository<Tag> tag,
            IPonenteTagRepository ponenteTag,
            IPonenteRepository ponente,
            IObjetivoActividadRepository objetivoActividad,
            ICommonRepository<ActividadPonente> actividadPonente,
            IMaterialActividadRepository materialActividad,
            IAzureStorage azureStorage,
            IInscripcionRepository inscripcion)
        {
            _context = context;
            Congreso = CongresoRepository;
            User = user;
            TipoIdentificacion = tipoIdentificacionRepository;
            TipoParticipante = tipoParticipante;
            CodigoVerificacion = codigoVerificacion;
            NivelAcademico = nivelAcademico;
            School = school;
            Actividad = actividad;
            TipoActividad = tipoActividad;
            RoleUsuario = roleUsuario;
            Role = role;
            Permisos = permisos;
            Menus = menus;
            RefreshToken = refreshToken;
            NivelDificultad = nivelDificultad;
            Tag = tag;
            PonenteTag = ponenteTag;
            Ponente = ponente;
            ObjetivoActividad = objetivoActividad;
            ActividadPonente = actividadPonente;
            MaterialActividad = materialActividad;
            this.azureStorage = azureStorage;
            Inscripcion = inscripcion;
        }

        public IDbTransaction BeginTransaction() => 
            _context.Database.BeginTransaction().GetDbTransaction();

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => 
            await _context.SaveChangesAsync(cancellationToken);

        public void Dispose() => _context.Dispose();
    }
}
