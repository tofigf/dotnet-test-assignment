namespace WeatherMcpServer.AI
{
    public interface IAiAdapter
    {
        string Name { get; }
        Task<string> GenerateSummaryAsync(string prompt, CancellationToken cancellationToken);
    }
}
