using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Actividades;
using congreso.Application.Dtos.Roles;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.Actividades.Queries.GetAll;

internal sealed class GetAllActividadesHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllActividadesQuery, IEnumerable<ActividadResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<ActividadResponseDto>>> Handle(GetAllActividadesQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ActividadResponseDto>>();

        try
        {
            var actividades = _unitOfWork.Actividad.GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrWhiteSpace(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        actividades = actividades.Where(r => r.Titulo!.Contains(query.TextFilter));
                        break;
                    case 2:
                        actividades = actividades.Where(r => r.Descripcion!.Contains(query.TextFilter));
                        break;
                    default:
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                actividades = actividades.Where(r => stateFilter.Contains(r.Estado!.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, actividades).ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await actividades.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<ActividadResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
