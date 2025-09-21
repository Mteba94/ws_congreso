using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;

internal sealed class GetAllNivelDificultadHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery, IFileLogger fileLogger) : IQueryHandler<GetAllNivelDificultadQuery, IEnumerable<NivelDificultdadResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<IEnumerable<NivelDificultdadResponseDto>>> Handle(GetAllNivelDificultadQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<NivelDificultdadResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetAllNivelDificultad", "0", query);

            var nivel = _unitOfWork.NivelDificultad
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
                nivel = nivel.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, nivel)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await nivel.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<NivelDificultdadResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetAllNivelDificultad", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetAllNivelDificultad", "1", response, ex.Message);
        }

        return response;
    }
}
