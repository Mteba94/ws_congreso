using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using System.Data;

namespace congreso.Application.Interfaces.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Congreso> Congreso { get; }
        IUserRepository User { get; }
        ITipoIdentificacionRepository TipoIdentificacion { get; }
        ITipoParticipanteRepository TipoParticipante { get; }
        ICodigoRepository CodigoVerificacion { get; }
        ICommonRepository<NivelAcademico> NivelAcademico { get; }
        ICommonRepository<School> School { get; }
        IGenericRepository<Ponente> Ponente { get; }
        IActividadRepository Actividad { get; }
        ICommonRepository<TipoActividad> TipoActividad { get; }
        ICommonRepository<RoleUsuario> RoleUsuario { get; }
        ICommonRepository<Role> Role { get; }
        IPermisosRepository Permisos { get; }
        IMenuRepository Menus { get; }
        IRefreshTokenRepository RefreshToken { get; }
        ICommonRepository<NivelDificultad> NivelDificultad { get; }
        ICommonRepository<Tag> Tag { get; }
        //ICommonRepository<PonenteTag> PonenteTag { get; }
        IPonenteTagRepository PonenteTag { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        IDbTransaction BeginTransaction();
    }
}
