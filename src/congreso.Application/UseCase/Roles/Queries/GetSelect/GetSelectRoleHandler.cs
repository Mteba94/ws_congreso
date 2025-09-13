using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Roles.Queries.GetSelect;

internal sealed class GetSelectRoleHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSelectRoleQuery, 
IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetSelectRoleQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

		try
		{
            var roles = await _unitOfWork.Role.GetAllAsync();

            if(roles is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_QUERY;
            response.Data = roles.Adapt<IEnumerable<SelectResponseDto>>();
        }
		catch (Exception ex)
		{
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;
    }
}
