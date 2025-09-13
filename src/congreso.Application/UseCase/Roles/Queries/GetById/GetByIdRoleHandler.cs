using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Roles;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Roles.Queries.GetById;

internal sealed class GetByIdRoleHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdRoleQuery, RoleByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<RoleByIdResponseDto>> Handle(GetByIdRoleQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<RoleByIdResponseDto>();

        try
        {
            var role = await _unitOfWork.Role.GetByIdAsync(query.RoleId);

            if (role is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = role.Adapt<RoleByIdResponseDto>();
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
