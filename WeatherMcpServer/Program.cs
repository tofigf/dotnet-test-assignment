using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherMcpServer.Application;
using WeatherMcpServer.AI;
using WeatherMcpServer.Tools;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services.AddSingleton<ITelemetryService, TelemetryService>();
builder.Services.AddScoped<IAiSummaryService, AiSummaryService>();

builder.Services.AddHttpClient<ClaudeAiAdapter>();
builder.Services.AddScoped<IAiAdapter, ClaudeAiAdapter>();

builder.Services
   .AddMcpServer()
   .WithStdioServerTransport()
   .WithTools<WeatherTools>()
   .WithTools<RandomNumberTools>();

await builder.Build().RunAsync();
