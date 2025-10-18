using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Actividades;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Actividades.Queries.GetParticipantsByActivityId;

internal sealed class GetParticipantsByActivityIdHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetParticipantsByActivityIdQuery, IEnumerable<ParticipantByActivityDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<IEnumerable<ParticipantByActivityDto>>> Handle(GetParticipantsByActivityIdQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetParticipantsByActivityIdAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<IEnumerable<ParticipantByActivityDto>>> GetParticipantsByActivityIdAsync(GetParticipantsByActivityIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ParticipantByActivityDto>>();

        try
        {
            var activity = await _unitOfWork.Actividad.GetByIdAsync(query.ActividadId);
            if (activity == null)
            {
                response.IsSuccess = false;
                response.Message = "Actividad no encontrada.";
                return response;
            }

            var inscriptions = (await _unitOfWork.Inscripcion.GetAllAsync())
                                .Where(i => i.ActividadId == query.ActividadId)
                                .ToList();

            if (!inscriptions.Any())
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var users = await _unitOfWork.User.GetAllAsync();

            var participants = from i in inscriptions
                               join u in users on i.UserId equals u.Id
                               select new ParticipantByActivityDto
                               {
                                   InscripcionId = i.Id,
                                   UserId = u.Id,
                                   UserName = u.Pnombre + " " + u.Papellido,
                                   UserEmail = u.Email,
                                   FechaInscripcion = i.FechaInscripcion,
                                   Puntaje = i.Puntaje,
                                   EsGanador = i.EsGanador
                               };

            response.IsSuccess = true;
            response.Data = participants;
            response.TotalRecords = participants.Count();
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
