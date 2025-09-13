using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.UserRoles;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Helper = congreso.Application.Helpers.Helpers;

namespace congreso.Application.UseCase.UserRoles.Queries.GetAll;

internal sealed class GetAllUserRoleHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllUserRoleQuery, IEnumerable<UserRoleResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<UserRoleResponseDto>>> Handle(GetAllUserRoleQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserRoleResponseDto>>();

        try
        {
            var userRoles = _unitOfWork.RoleUsuario
                .GetAllQueryable()
                .Include(x => x.User)
                .Include(x => x.Role)
                .AsQueryable();

            if(query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        userRoles = userRoles.Where(x => x.User.Pnombre.Contains(query.TextFilter));
                        break;
                    case 2:
                        userRoles = userRoles.Where(x => x.Role.Nombre.Contains(query.TextFilter));
                        break;
                }
            }

            if(query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                userRoles = userRoles.Where(x => stateFilter.Contains(x.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, userRoles).ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await userRoles.CountAsync(cancellationToken);
            response.Message = ReplyMessage.MESSAGE_QUERY;
            response.Data = items.Adapt<IEnumerable<UserRoleResponseDto>>();

        }
        catch(Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
