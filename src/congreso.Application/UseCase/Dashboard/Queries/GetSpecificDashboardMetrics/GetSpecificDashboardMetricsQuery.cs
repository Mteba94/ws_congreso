using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;

namespace congreso.Application.UseCase.Dashboard.Queries.GetSpecificDashboardMetrics;

public sealed record GetSpecificDashboardMetricsQuery : IQuery<SpecificDashboardMetricsDto>;
