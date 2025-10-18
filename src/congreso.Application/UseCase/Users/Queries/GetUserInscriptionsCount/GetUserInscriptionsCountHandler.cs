using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Users.Queries.GetUserInscriptionsCount;

internal sealed class GetUserInscriptionsCountHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetUserInscriptionsCountQuery, UserInscriptionsCountDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<UserInscriptionsCountDto>> Handle(GetUserInscriptionsCountQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetUserInscriptionsCountAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<UserInscriptionsCountDto>> GetUserInscriptionsCountAsync(GetUserInscriptionsCountQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserInscriptionsCountDto>();

        try
        {
            var inscriptions = await _unitOfWork.Inscripcion.GetAllAsync();

            if (inscriptions == null || !inscriptions.Any())
            {
                response.IsSuccess = true;
                response.Data = new UserInscriptionsCountDto { UserId = query.UserId, InscriptionsCount = 0 };
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var inscriptionsCount = inscriptions.Count(i => i.UserId == query.UserId);

            response.IsSuccess = true;
            response.Data = new UserInscriptionsCountDto { UserId = query.UserId, InscriptionsCount = inscriptionsCount };
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
