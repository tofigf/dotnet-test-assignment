using System.Collections.Concurrent;
using WeatherMcpServer.Domain;

namespace WeatherMcpServer.Application
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ConcurrentDictionary<string, List<TelemetryData>> _data = new();

        public void Add(TelemetryData data)
        {
            var list = _data.GetOrAdd(data.StationId, _ => new());
            lock (list)
            {
                list.Add(data);
            }
        }

        public IEnumerable<TelemetryData> GetByStationId(string stationId)
        {
            return _data.TryGetValue(stationId, out var list) ? list : Enumerable.Empty<TelemetryData>();
        }

        public TelemetryStats GetStats(string stationId)
        {
            var data = GetByStationId(stationId).ToList();
            if (!data.Any()) return new();

            return new TelemetryStats
            {
                Temperature = CreateStats(data.Select(d => d.Temperature)),
                Humidity = CreateStats(data.Select(d => d.Humidity)),
                Pressure = CreateStats(data.Select(d => d.Pressure)),
            };
        }

        private StatResult CreateStats(IEnumerable<float> values)
        {
            var list = values.ToList();
            return new StatResult
            {
                Min = list.Min(),
                Max = list.Max(),
                Avg = list.Average()
            };
        }
    }
}
