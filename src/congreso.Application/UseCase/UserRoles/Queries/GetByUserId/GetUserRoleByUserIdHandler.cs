using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Roles;
using congreso.Application.Dtos.Tags;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.UserRoles.Queries.GetByUserId;

internal sealed class GetUserRoleByUserIdHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUserRoleByUserIdQuery, RoleByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<RoleByIdResponseDto>> Handle(GetUserRoleByUserIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<RoleByIdResponseDto>();

        try
        {
            var userRole = await _unitOfWork.RoleUsuario.GetByUserId(query.UserId);

            if (userRole is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            var role = await _unitOfWork.Role.GetByIdAsync(userRole.RoleId);

            response.IsSuccess = true;
            response.Data = role.Adapt<RoleByIdResponseDto>();
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
