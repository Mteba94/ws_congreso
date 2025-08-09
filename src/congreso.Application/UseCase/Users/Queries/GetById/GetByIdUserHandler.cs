using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Users.Queries.GetById;

internal sealed class GetByIdUserHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetByIdUserQuery, UserByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<UserByIdResponseDto>> Handle(GetByIdUserQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserByIdResponseDto>();

        try
        {
            var user = await _unitOfWork.User.GetByIdAsync(query.UserId);

            if(user is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = user.Adapt<UserByIdResponseDto>();
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex) 
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
