using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TiposActividad;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.TiposActividad.Queries.GetAll;

internal sealed class GetAllTipoActividadHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery, IFileLogger fileLogger) : IQueryHandler<GetAllTipoActividadQuery, IEnumerable<TipoActividadResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<IEnumerable<TipoActividadResponseDto>>> Handle(GetAllTipoActividadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TipoActividadResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetAllTipoActividad", "0", query);

            var tiposActividad = _unitOfWork.TipoActividad
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
                tiposActividad = tiposActividad.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, tiposActividad)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await tiposActividad.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<TipoActividadResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetAllTipoActividad", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetAllTipoActividad", "1", response, ex.Message);
        }

        return response;
    }
}
