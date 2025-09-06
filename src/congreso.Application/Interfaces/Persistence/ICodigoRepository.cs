using congreso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.Interfaces.Persistence
{
    public interface ICodigoRepository : ICommonRepository<CodigoVerificacion>
    {
        Task<CodigoVerificacion> ValidarCodigoAsync(int userId, string purpose);
    }
}
