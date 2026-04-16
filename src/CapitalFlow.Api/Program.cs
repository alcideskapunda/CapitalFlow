global using FastEndpoints;
using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Configuration;
using CapitalFlow.Infrastructure;
using CapitalFlow.Infrastructure.Configuration.Hangfire;
using CapitalFlow.Persistence.DependencyInjection;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;

namespace CapitalFlow.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.ConfigureLogging();
            ConfigureCors(builder);

            builder.Services.AddAuthenticationJwtBearer(options =>
                options.SigningKey = builder.Configuration["Jwt:SigningKey"]);

            builder.Services.AddCapitalFlowAuthorization();
            builder.Services.AddFastEndpoints().SwaggerDocument(o =>
            {
                // With explicit .WithTags(...) on endpoints, path-segment auto-tagging duplicates each operation in OpenAPI/Scalar.
                o.AutoTagPathSegmentIndex = 0;
            });
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
        }

        var app = builder.Build();
        {
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseFastEndpoints(config => config.Errors.UseProblemDetails());

            if (app.Environment.IsDevelopment())
            {
                // NSwag serves the OpenAPI document; FastEndpoints metadata (Summary.Params, XML docs, etc.) is included here.
                // Microsoft.AspNetCore.OpenApi (MapOpenApi) does not integrate with FastEndpoints and omits most FE-specific docs.
                var baseUrl = builder.Configuration["ApiBaseUrl"];

                app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");

                app.MapScalarApiReference(o =>
                {
                    o.AddDocument("v1");
                    o.SortTagsAlphabetically();
                    o.SortOperationsByMethod();
                });
            }
        }

        app.UseHangfireDashboard();
        app.Run();
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}