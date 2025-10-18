using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;

namespace congreso.Application.UseCase.Dashboard.Queries.GetChartsData;

public sealed record GetChartsDataQuery : IQuery<ChartsDataDto>;
