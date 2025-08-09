using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.Users.Queries.GetAllUser;
internal sealed class GetAllUserHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllUserQuery, IEnumerable<UserResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    public async Task<BaseResponse<IEnumerable<UserResponseDto>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserResponseDto>>();

        try
        {
            var users = _unitOfWork.User.GetAllQueryable();


            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        //users = users.Where(u => u.Pnombre.Contains(query.TextFilter));
                        break;
                }
            }

            if(query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                users = users.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, users)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await users.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<UserResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch(Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
