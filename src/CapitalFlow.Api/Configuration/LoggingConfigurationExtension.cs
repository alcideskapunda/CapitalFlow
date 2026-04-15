using Serilog;

namespace CapitalFlow.Api.Configuration;

public static class LoggingConfigurationExtension
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "CapitalFlow.Api")
            .WriteTo.Console()
            .CreateLogger();

        builder.Services.AddSerilog();
    }
}
