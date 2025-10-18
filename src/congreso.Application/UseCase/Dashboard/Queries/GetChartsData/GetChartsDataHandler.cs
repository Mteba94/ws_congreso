using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using System.Linq;

namespace congreso.Application.UseCase.Dashboard.Queries.GetChartsData;

internal sealed class GetChartsDataHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetChartsDataQuery, ChartsDataDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<ChartsDataDto>> Handle(GetChartsDataQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetChartsDataAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<ChartsDataDto>> GetChartsDataAsync(GetChartsDataQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ChartsDataDto>();

        try
        {
            var allActivities = await _unitOfWork.Actividad.GetAllAsync();
            var allAttendances = await _unitOfWork.Asistencia.GetAllAsync();
            var allUsers = await _unitOfWork.User.GetAllAsync();
            var allTipoParticipante = await _unitOfWork.TipoParticipante.GetAllAsync();

            // 1. Activity Data: Activities with their total attendance
            var activityData = allActivities.Select(a => new ActivityChartDataDto
            {
                Name = a.Titulo,
                Attendance = allAttendances.Count(att => att.ActividadId == a.Id)
            }).ToList();

            // 2. Hourly Data: Participants per hour
            var hourlyData = allAttendances
                .GroupBy(att => att.FechaRegistro.Hour)
                .OrderBy(g => g.Key)
                .Select(g => new HourlyChartDataDto
                {
                    Hour = $"{g.Key:00}:00",
                    Participants = g.Count()
                }).ToList();

            // 3. Demographics Data: Participants by TipoParticipante
            var demographicsData = allUsers
                .Where(u => u.TipoParticipanteId.HasValue)
                .GroupBy(u => u.TipoParticipanteId)
                .Select(g => new DemographicsChartDataDto
                {
                    Name = allTipoParticipante.FirstOrDefault(tp => tp.Id == g.Key)?.Nombre ?? "Desconocido",
                    Value = g.Count()
                }).ToList();

            response.IsSuccess = true;
            response.Data = new ChartsDataDto
            {
                ActivityData = activityData,
                HourlyData = hourlyData,
                DemographicsData = demographicsData
            };
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
