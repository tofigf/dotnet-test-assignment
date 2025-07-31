using ModelContextProtocol.Server;
using WeatherMcpServer.AI;
using WeatherMcpServer.Application;

namespace WeatherMcpServer.Tools;

public class WeatherTools
{
    private readonly ITelemetryService _telemetryService;
    private readonly IAiSummaryService _summaryService;

    public WeatherTools(ITelemetryService telemetryService, IAiSummaryService summaryService)
    {
        _telemetryService = telemetryService;
        _summaryService = summaryService;
    }

    [McpServerTool]

    public async Task<string> GetSummary(string stationId)
    {
        var telemetry = _telemetryService.GetByStationId(stationId).ToList();
        if (!telemetry.Any())
            return "No telemetry data found.";

        var summary = await _summaryService.GenerateSummaryAsync(stationId, telemetry, "Claude", CancellationToken.None);
        return summary;
    }
}

