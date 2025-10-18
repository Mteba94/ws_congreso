using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Application.UseCase.Dashboard.Queries.GetTotalActivities;

internal sealed class GetTotalActivitiesHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetTotalActivitiesQuery, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<int>> Handle(GetTotalActivitiesQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetTotalActivitiesAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<int>> GetTotalActivitiesAsync(GetTotalActivitiesQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var totalActivities = await _unitOfWork.Actividad.GetAllAsync();
            response.IsSuccess = true;
            response.Data = totalActivities.Count();
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