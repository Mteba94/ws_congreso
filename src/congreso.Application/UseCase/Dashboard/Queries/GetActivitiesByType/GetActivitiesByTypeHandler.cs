using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Application.UseCase.Dashboard.Queries.GetActivitiesByType;

internal sealed class GetActivitiesByTypeHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetActivitiesByTypeQuery, IEnumerable<ActivityTypeCountDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<IEnumerable<ActivityTypeCountDto>>> Handle(GetActivitiesByTypeQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetActivitiesByTypeAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<IEnumerable<ActivityTypeCountDto>>> GetActivitiesByTypeAsync(GetActivitiesByTypeQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ActivityTypeCountDto>>();

        try
        {
            // Assuming Actividad.GetAllAsync() can include TipoActividad or TipoActividad can be fetched separately.
            // For optimal performance, a specific repository method might be needed to join and group in the database.
            var activities = await _unitOfWork.Actividad.GetAllAsync();
            var tipoActividades = await _unitOfWork.TipoActividad.GetAllAsync(); // Fetch all types

            if (activities == null || !activities.Any() || tipoActividades == null || !tipoActividades.Any())
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var result = activities
                .GroupBy(a => a.TipoActividadId)
                .Select(g => new ActivityTypeCountDto
                {
                    TipoActividadName = tipoActividades.FirstOrDefault(t => t.Id == g.Key)?.Nombre ?? "Desconocido",
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
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