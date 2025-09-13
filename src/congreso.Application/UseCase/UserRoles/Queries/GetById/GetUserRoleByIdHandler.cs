using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.UserRoles;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.UserRoles.Queries.GetById;

internal sealed class GetUserRoleByIdHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUserRoleByIdQuery, UserRoleByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<UserRoleByIdResponseDto>> Handle(GetUserRoleByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserRoleByIdResponseDto>();

        try
        {
            var userRole = await _unitOfWork.RoleUsuario.GetByIdAsync(query.UserRoleId);

            if (userRole is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = userRole.Adapt<UserRoleByIdResponseDto>();
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
