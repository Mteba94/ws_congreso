using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.UseCase.Dashboard.Queries.GetActivitiesByType;
using congreso.Application.UseCase.Dashboard.Queries.GetParticipantsByActivity;
using congreso.Application.UseCase.Dashboard.Queries.GetTotalActivities;
using congreso.Application.UseCase.Dashboard.Queries.GetTotalParticipants;
using congreso.Application.UseCase.Dashboard.Queries.GetTopWinnersOfLastActivities;
using congreso.Application.UseCase.Dashboard.Queries.GetGlobalDashboardSummary;
using congreso.Application.UseCase.Dashboard.Queries.GetSpecificDashboardMetrics;
using congreso.Application.UseCase.Dashboard.Queries.GetChartsData;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("TotalActivities")]
        public async Task<IActionResult> GetTotalActivities()
        {
            var response = await _dispatcher
                .Dispatch<GetTotalActivitiesQuery, int>(new GetTotalActivitiesQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("TotalParticipants")]
        public async Task<IActionResult> GetTotalParticipants()
        {
            var response = await _dispatcher
                .Dispatch<GetTotalParticipantsQuery, int>(new GetTotalParticipantsQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ActivitiesByType")]
        public async Task<IActionResult> GetActivitiesByType()
        {
            var response = await _dispatcher
                .Dispatch<GetActivitiesByTypeQuery, IEnumerable<ActivityTypeCountDto>>(new GetActivitiesByTypeQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ParticipantsByActivity")]
        public async Task<IActionResult> GetParticipantsByActivity()
        {
            var response = await _dispatcher
                .Dispatch<GetParticipantsByActivityQuery, IEnumerable<ParticipantsByActivityDto>>(new GetParticipantsByActivityQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("TopWinnersLastActivities")]
        public async Task<IActionResult> GetTopWinnersLastActivities()
        {
            var response = await _dispatcher
                .Dispatch<GetTopWinnersOfLastActivitiesQuery, IEnumerable<TopWinnerDto>>(new GetTopWinnersOfLastActivitiesQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GlobalSummary")]
        public async Task<IActionResult> GetGlobalSummary([FromQuery] string? dateRangeFilter = null)
        {
            var response = await _dispatcher
                .Dispatch<GetGlobalDashboardSummaryQuery, GlobalDashboardSummaryDto>(new GetGlobalDashboardSummaryQuery(dateRangeFilter), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("SpecificMetrics")]
        public async Task<IActionResult> GetSpecificMetrics()
        {
            var response = await _dispatcher
                .Dispatch<GetSpecificDashboardMetricsQuery, SpecificDashboardMetricsDto>(new GetSpecificDashboardMetricsQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ChartsData")]
        public async Task<IActionResult> GetChartsData()
        {
            var response = await _dispatcher
                .Dispatch<GetChartsDataQuery, ChartsDataDto>(new GetChartsDataQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}