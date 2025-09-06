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
        public IGenericRepository<Ponente> Ponente { get; }
        public IActividadRepository Actividad { get; }
        public ICommonRepository<TipoActividad> TipoActividad { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IGenericRepository<Congreso> CongresoRepository,
            IUserRepository user,
            ITipoIdentificacionRepository tipoIdentificacionRepository,
            ITipoParticipanteRepository tipoParticipante,
            ICodigoRepository codigoVerificacion,
            ICommonRepository<NivelAcademico> nivelAcademico,
            ICommonRepository<School> school,
            IGenericRepository<Ponente> ponente,
            IActividadRepository actividad,
            ICommonRepository<TipoActividad> tipoActividad)
        {
            _context = context;
            Congreso = CongresoRepository;
            User = user;
            TipoIdentificacion = tipoIdentificacionRepository;
            TipoParticipante = tipoParticipante;
            CodigoVerificacion = codigoVerificacion;
            NivelAcademico = nivelAcademico;
            School = school;
            Ponente = ponente;
            Actividad = actividad;
            TipoActividad = tipoActividad;
        }

        public IDbTransaction BeginTransaction() => 
            _context.Database.BeginTransaction().GetDbTransaction();

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => 
            await _context.SaveChangesAsync(cancellationToken);

        public void Dispose() => _context.Dispose();
    }
}
