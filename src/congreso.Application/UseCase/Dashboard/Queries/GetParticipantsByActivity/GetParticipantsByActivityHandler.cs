using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Dashboard.Queries.GetParticipantsByActivity;

internal sealed class GetParticipantsByActivityHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetParticipantsByActivityQuery, IEnumerable<ParticipantsByActivityDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<IEnumerable<ParticipantsByActivityDto>>> Handle(GetParticipantsByActivityQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetParticipantsByActivityAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<IEnumerable<ParticipantsByActivityDto>>> GetParticipantsByActivityAsync(GetParticipantsByActivityQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ParticipantsByActivityDto>>();

        try
        {
            var inscriptions = await _unitOfWork.Inscripcion.GetAllAsync();
            var activities = await _unitOfWork.Actividad.GetAllAsync();

            if (inscriptions == null || !inscriptions.Any() || activities == null || !activities.Any())
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var result = inscriptions
                .GroupBy(i => i.ActividadId)
                .Select(g => new ParticipantsByActivityDto
                {
                    ActivityName = activities.FirstOrDefault(a => a.Id == g.Key)?.Titulo ?? "Desconocido",
                    ParticipantCount = g.Count()
                })
                .OrderByDescending(x => x.ParticipantCount)
                .ToList();

            response.IsSuccess = true;
            response.Data = result;
            response.TotalRecords = result.Count;
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
