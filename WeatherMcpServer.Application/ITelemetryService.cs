using WeatherMcpServer.Domain;

namespace WeatherMcpServer.Application
{
    public interface ITelemetryService
    {
        void Add(TelemetryData data);
        IEnumerable<TelemetryData> GetByStationId(string stationId);
        TelemetryStats GetStats(string stationId);
    }
}
