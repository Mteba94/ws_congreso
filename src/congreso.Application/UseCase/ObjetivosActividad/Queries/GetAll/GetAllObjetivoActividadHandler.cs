using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.ObjetivosActividad;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.ObjetivosActividad.Queries.GetAll;

internal sealed class GetAllObjetivoActividadHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllObjetivoActividadQuery, IEnumerable<ObjetivosActividadResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    public async Task<BaseResponse<IEnumerable<ObjetivosActividadResponseDto>>> Handle(GetAllObjetivoActividadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ObjetivosActividadResponseDto>>();

        try
        {
            var objetivosActividad = _unitOfWork.ObjetivoActividad
                .GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        //users = users.Where(u => u.Pnombre.Contains(query.TextFilter));
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                objetivosActividad = objetivosActividad.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, objetivosActividad)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await objetivosActividad.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<ObjetivosActividadResponseDto>>();
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
