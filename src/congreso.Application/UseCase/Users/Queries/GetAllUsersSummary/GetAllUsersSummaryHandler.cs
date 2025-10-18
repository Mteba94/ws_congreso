using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Users.Queries.GetAllUsersSummary;

internal sealed class GetAllUsersSummaryHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetAllUsersSummaryQuery, IEnumerable<UserSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<IEnumerable<UserSummaryDto>>> Handle(GetAllUsersSummaryQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetAllUsersSummaryAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<IEnumerable<UserSummaryDto>>> GetAllUsersSummaryAsync(GetAllUsersSummaryQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserSummaryDto>>();

        try
        {
            var users = await _unitOfWork.User.GetAllAsync();
            var inscriptions = await _unitOfWork.Inscripcion.GetAllAsync();
            var diplomas = await _unitOfWork.Diploma.GetAllAsync();

            if (users == null || !users.Any())
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var userSummaries = users.Select(u => new UserSummaryDto
            {
                UserId = u.Id,
                UserName = u.Pnombre + " " + u.Papellido,
                InscriptionsCount = inscriptions.Count(i => i.UserId == u.Id),
                CertificatesCount = diplomas.Count(d => inscriptions.Any(i => i.Id == d.InscripcionId && i.UserId == u.Id))
            }).ToList();

            response.IsSuccess = true;
            response.Data = userSummaries;
            response.TotalRecords = userSummaries.Count;
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
