using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Congreso;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.Congresos.Queries.GetAllCongreso;

internal sealed class GetAllCongresoHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery, IFileLogger fileLogger) : IQueryHandler<GetAllCongresoQuery, IEnumerable<CongresoResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<IEnumerable<CongresoResponseDto>>> Handle(GetAllCongresoQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CongresoResponseDto>>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetAllCongreso", "0", query);

            var congreso = _unitOfWork.Congreso.GetAllQueryable();

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
                congreso = congreso.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, congreso)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await congreso.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<CongresoResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetAllCongreso", "1", response);

        }
        catch (Exception ex)
        {

        }

        return response;
    }
}
