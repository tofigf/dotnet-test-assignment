using System.Text;
using WeatherMcpServer.Domain;

namespace WeatherMcpServer.AI
{
    public class AiSummaryService : IAiSummaryService
    {
        private readonly IEnumerable<IAiAdapter> _adapters;

        public AiSummaryService(IEnumerable<IAiAdapter> adapters)
        {
            _adapters = adapters;
        }

        public async Task<string> GenerateSummaryAsync(string stationId, IEnumerable<TelemetryData> data, string provider, CancellationToken cancellationToken)
        {
            var adapter = _adapters.FirstOrDefault(a => a.Name.Equals(provider, StringComparison.OrdinalIgnoreCase))
                          ?? throw new Exception($"AI provider '{provider}' is not registered.");

            var sb = new StringBuilder();
            sb.AppendLine($"Generate a human-readable weather report for station '{stationId}'.");
            sb.AppendLine("Each record contains: timestamp, temperature (°C), humidity (%), and pressure (hPa):");

            foreach (var record in data.OrderBy(x => x.Timestamp))
            {
                sb.AppendLine($"[{record.Timestamp:O}] Temp: {record.Temperature}, Hum: {record.Humidity}, Pres: {record.Pressure}");
            }

            return await adapter.GenerateSummaryAsync(sb.ToString(), cancellationToken);
        }
    }
}
