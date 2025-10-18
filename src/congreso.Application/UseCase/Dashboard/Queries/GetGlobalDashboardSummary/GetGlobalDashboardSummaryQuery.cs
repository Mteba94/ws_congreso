using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;

namespace congreso.Application.UseCase.Dashboard.Queries.GetGlobalDashboardSummary;

public sealed record GetGlobalDashboardSummaryQuery(string? DateRangeFilter = null) : IQuery<GlobalDashboardSummaryDto>;
