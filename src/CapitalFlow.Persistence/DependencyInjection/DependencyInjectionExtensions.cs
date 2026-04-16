using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Database;
using CapitalFlow.Persistence.Entities.Users.Users;
using CapitalFlow.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalFlow.Persistence.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDatabase(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        // 👉 quando criar tiver repositories, adiciona aqui
        services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<ICustomerRepository, CustomerRepository>();
        // services.AddScoped<IBuyOrderRepository, BuyOrderRepository>();

        // services.AddScoped<AuditingInterceptor>();
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<CapitalFlowDbContext>(
            options =>
            {
                options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    mysqlOptions =>
                        mysqlOptions
                            .EnableRetryOnFailure()
                            .TranslateParameterizedCollectionsToConstants());

                // DEV ONLY
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            },
            contextLifetime: ServiceLifetime.Scoped,
            optionsLifetime: ServiceLifetime.Singleton);
    }
}
