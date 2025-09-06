using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.TiposIdentificacion.Queries.GetAll;

internal sealed class GetAllTipoIdentificacionHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery, IFileLogger fileLogger) : IQueryHandler<GetAllTipoIdentificacionQuery, IEnumerable<TipoIdentificacionResponseDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<IEnumerable<TipoIdentificacionResponseDTO>>> Handle(GetAllTipoIdentificacionQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TipoIdentificacionResponseDTO>>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetAllTipoIdentificacion", "0", query);

            var tiposIdentificacion = _unitOfWork.TipoIdentificacion.GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        //tiposIdentificacion = tiposIdentificacion.Where(t => t.Nombre.Contains(query.TextFilter));
                        break;
                    case 2:
                        //tiposIdentificacion = tiposIdentificacion.
                        break;
                }
            }
            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                //tiposIdentificacion = tiposIdentificacion.Where(t => stateFilter.Contains(t.Estado.ToString()));
            }
            query.Sort ??= "Id";
            var items = await _orderingQuery.Ordering(query, tiposIdentificacion).ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = tiposIdentificacion.Count();
            response.Data = items.Adapt<IEnumerable<TipoIdentificacionResponseDTO>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetAllTipoIdentificacion", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            _fileLogger.Log("ws_congreso", "GetAllTipoIdentificacion", "1", response, ex.Message);
        }
        return response;
    }
}
