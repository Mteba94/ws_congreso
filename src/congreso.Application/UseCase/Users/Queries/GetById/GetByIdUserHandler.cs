using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System.Text.Json;

namespace congreso.Application.UseCase.Users.Queries.GetById;

internal sealed class GetByIdUserHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<GetByIdUserQuery, UserByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<UserByIdResponseDto>> Handle(GetByIdUserQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserByIdResponseDto>();

        try
        {
            _fileLogger.Log("ws_congreso", "GetByIdUser", "0", query);

            var user = await _unitOfWork.User.GetByIdAsync(query.UserId);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "GetByIdUser", "1", response);

                return response;
            }

            response.IsSuccess = true;
            response.Data = user.Adapt<UserByIdResponseDto>();
            response.Message = ReplyMessage.MESSAGE_QUERY;

            _fileLogger.Log("ws_congreso", "GetByIdUser", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "GetByIdUser", "1", response, ex.Message);
        }

        return response;
    }
}
