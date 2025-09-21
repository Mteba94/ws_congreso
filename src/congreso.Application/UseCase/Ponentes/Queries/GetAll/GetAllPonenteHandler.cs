using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Ponentes;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.Ponentes.Queries.GetAll;

internal sealed class GetAllPonenteHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllPonenteQuery, IEnumerable<PonenteResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    public async Task<BaseResponse<IEnumerable<PonenteResponseDto>>> Handle(GetAllPonenteQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PonenteResponseDto>>();

        try
        {
            var ponentes = _unitOfWork.Ponente
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
                ponentes = ponentes.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, ponentes)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await ponentes.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<PonenteResponseDto>>();
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
