using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Dashboard.Queries.GetTopWinnersOfLastActivities;

internal sealed class GetTopWinnersOfLastActivitiesHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetTopWinnersOfLastActivitiesQuery, IEnumerable<TopWinnerDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<IEnumerable<TopWinnerDto>>> Handle(GetTopWinnersOfLastActivitiesQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetTopWinnersOfLastActivitiesAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<IEnumerable<TopWinnerDto>>> GetTopWinnersOfLastActivitiesAsync(GetTopWinnersOfLastActivitiesQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TopWinnerDto>>();

        try
        {
            var allActivities = await _unitOfWork.Actividad.GetAllAsync();
            var allInscriptions = await _unitOfWork.Inscripcion.GetAllAsync();
            var allUsers = await _unitOfWork.User.GetAllAsync();

            // 1. Get last 3 completed activities
            var lastCompletedActivities = allActivities
                .Where(a => a.EstadoActividad == "f") // 'f' for Finalizado
                .OrderByDescending(a => a.FechaActividad) // Assuming FechaActividad is relevant for 'last'
                .Take(3)
                .ToList();

            if (!lastCompletedActivities.Any())
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var topWinners = new List<TopWinnerDto>();

            foreach (var activity in lastCompletedActivities)
            {
                var winnerInscription = allInscriptions
                    .Where(i => i.ActividadId == activity.Id && i.EsGanador == true && i.Puntaje.HasValue)
                    .OrderByDescending(i => i.Puntaje)
                    .FirstOrDefault();

                if (winnerInscription != null)
                {
                    var winnerUser = allUsers.FirstOrDefault(u => u.Id == winnerInscription.UserId);

                    topWinners.Add(new TopWinnerDto
                    {
                        ActividadId = activity.Id,
                        ActividadTitulo = activity.Titulo,
                        WinnerUserId = winnerUser?.Id,
                        WinnerUserName = winnerUser?.Pnombre + " " + winnerUser?.Papellido,
                        WinnerScore = winnerInscription.Puntaje
                    });
                }
                else
                {
                    // If no winner found for a completed activity, still add an entry
                    topWinners.Add(new TopWinnerDto
                    {
                        ActividadId = activity.Id,
                        ActividadTitulo = activity.Titulo,
                        WinnerUserId = null,
                        WinnerUserName = "N/A",
                        WinnerScore = null
                    });
                }
            }

            response.IsSuccess = true;
            response.Data = topWinners;
            response.TotalRecords = topWinners.Count;
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
