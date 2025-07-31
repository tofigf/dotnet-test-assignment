using WeatherMcpServer.Domain;

namespace WeatherMcpServer.AI
{
    public interface IAiSummaryService
    {
        Task<string> GenerateSummaryAsync(string stationId, IEnumerable<TelemetryData> data, string provider, CancellationToken cancellationToken);
    }
}
