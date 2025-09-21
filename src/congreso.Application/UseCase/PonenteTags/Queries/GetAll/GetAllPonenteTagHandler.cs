using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.PonenteTags;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.PonenteTags.Queries.GetAll;

internal sealed class GetAllPonenteTagHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllPonenteTagQuery, IEnumerable<PonenteTagsResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    public async Task<BaseResponse<IEnumerable<PonenteTagsResponseDto>>> Handle(GetAllPonenteTagQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PonenteTagsResponseDto>>();

        try
        {
            var ponenteTags = _unitOfWork.PonenteTag
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
                ponenteTags = ponenteTags.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, ponenteTags)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await ponenteTags.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<PonenteTagsResponseDto>>();
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
