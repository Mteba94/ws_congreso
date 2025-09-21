using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TiposActividad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.TiposActividad.Queries.GetAll;

public class GetAllTipoActividadQuery : BaseFilters, IQuery<IEnumerable<TipoActividadResponseDto>>
{
}
