namespace congreso.Application.Dtos.Dashboard;

public class ActivityChartDataDto
{
    public string Name { get; set; } = null!;
    public int Attendance { get; set; }
}

public class HourlyChartDataDto
{
    public string Hour { get; set; } = null!;
    public int Participants { get; set; }
}

public class DemographicsChartDataDto
{
    public string Name { get; set; } = null!;
    public int Value { get; set; }
}

public class ChartsDataDto
{
    public IEnumerable<ActivityChartDataDto> ActivityData { get; set; } = new List<ActivityChartDataDto>();
    public IEnumerable<HourlyChartDataDto> HourlyData { get; set; } = new List<HourlyChartDataDto>();
    public IEnumerable<DemographicsChartDataDto> DemographicsData { get; set; } = new List<DemographicsChartDataDto>();
}
