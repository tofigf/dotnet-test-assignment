namespace WeatherMcpServer.Domain
{
    public class TelemetryStats
    {
        public StatResult Temperature { get; set; } = new();
        public StatResult Humidity { get; set; } = new();
        public StatResult Pressure { get; set; } = new();
    }
}
