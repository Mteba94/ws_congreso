using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Application.UseCase.Dashboard.Queries.GetTotalParticipants;

internal sealed class GetTotalParticipantsHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetTotalParticipantsQuery, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<int>> Handle(GetTotalParticipantsQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetTotalParticipantsAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<int>> GetTotalParticipantsAsync(GetTotalParticipantsQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var totalParticipants = await _unitOfWork.Inscripcion.GetAllAsync();
            response.IsSuccess = true;
            response.Data = totalParticipants.Count();
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