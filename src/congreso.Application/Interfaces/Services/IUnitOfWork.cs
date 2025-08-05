using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.Interfaces.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Congreso> Congreso { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        IDbTransaction BeginTransaction();
    }
}
