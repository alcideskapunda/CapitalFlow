using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CapitalFlow.Infrastructure.Configuration.Hangfire;

public static class HangfireConfigurationExtension
{
    public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(hangFireConfiguration =>
        {
            Log.Information("Configuring Hangfire...");
            hangFireConfiguration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            hangFireConfiguration.UseSimpleAssemblyNameTypeSerializer();
            hangFireConfiguration.UseRecommendedSerializerSettings();
            hangFireConfiguration.UseSerilogLogProvider();

            var connectionString = configuration.GetConnectionString("HangfireConnection");

            Log.Information("Using MySql storage");
            ConfigureHangFireMySqlStorage(hangFireConfiguration, connectionString!);

            Log.Information("Hangfire configured");
        });

        services.AddHangfireServer();
        return services;
    }

    private static void ConfigureHangFireMySqlStorage(IGlobalConfiguration hangFireConfiguration,
        string connectionString)
    {
        hangFireConfiguration.UseStorage(new MySqlStorage(connectionString,
            new MySqlStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                TablesPrefix = "Hangfire_",
                QueuePollInterval = TimeSpan.FromSeconds(30),
            }));
    }

    public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app)
    {
        HangFireSecrets secrets = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSection("Hangfire")
            .Get<HangFireSecrets>();
        var options = new DashboardOptions
        {
            Authorization =
            [
                new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    SslRedirect = false,
                    RequireSsl = false,
                    LoginCaseSensitive = true,
                    Users =
                    [
                        new BasicAuthAuthorizationUser
                        {
                            Login = secrets.Login,
                            PasswordClear = secrets.Password
                        }
                    ]
                })
            ]
        };

        app.UseHangfireDashboard($"/{secrets.RouteName}", options);
        return app;
    }
}
