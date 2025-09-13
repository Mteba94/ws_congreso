using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Roles;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.Roles.Queries.GetAll;

internal sealed class GetAllRoleHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllRoleQuery, IEnumerable<RoleResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<RoleResponseDto>>> Handle(GetAllRoleQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<RoleResponseDto>>();

        try
        {
            var roles = _unitOfWork.Role.GetAllQueryable();

            if(query.NumFilter is not null && !string.IsNullOrWhiteSpace(query.TextFilter))
            {
                switch(query.NumFilter)
                {
                    case 1:
                        roles = roles.Where(r => r.Nombre!.Contains(query.TextFilter));
                        break;
                    case 2:
                        roles = roles.Where(r => r.Descripcion!.Contains(query.TextFilter));
                        break;
                    default:
                        break;
                }
            }

            if(query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                roles = roles.Where(r => stateFilter.Contains(r.Estado!.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, roles).ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await roles.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<RoleResponseDto>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch(Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
