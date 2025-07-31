namespace WeatherMcpServer.Domain
{
    public class TelemetryData
    {
        public string StationId { get; set; } = default!;
        public DateTime Timestamp { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
    }
}
