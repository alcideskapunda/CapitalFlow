using CapitalFlow.Application.Features.Users.Auth;
using CapitalFlow.Infrastructure.Configuration.Hangfire;
using CapitalFlow.Infrastructure.Features.Users.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        services.AddHangfire(configuration);

        return services;
    }
}
