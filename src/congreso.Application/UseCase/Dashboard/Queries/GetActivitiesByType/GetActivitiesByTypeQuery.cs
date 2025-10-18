using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;

namespace congreso.Application.UseCase.Dashboard.Queries.GetActivitiesByType;

public sealed class GetActivitiesByTypeQuery : IQuery<IEnumerable<ActivityTypeCountDto>>
{
}