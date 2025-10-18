using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;

namespace congreso.Application.UseCase.Dashboard.Queries.GetTopWinnersOfLastActivities;

public sealed record GetTopWinnersOfLastActivitiesQuery : IQuery<IEnumerable<TopWinnerDto>>;
