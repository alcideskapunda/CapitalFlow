using CapitalFlow.Api.Features.Common.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Users.Auth.Login;

public sealed class LoginEndpoint : Endpoint<LoginCommand, Results<Ok<LoginResponse>, ProblemDetails>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("/users/login");
        Summary(s =>
        {
            s.Summary = "Authenticate and issue JWT";
            s.Description = "Validates credentials and returns a JWT for subsequent requests. The identifier may be the user's email address or a 9-digit phone number (digits only). Login is denied if a password reset is required, the email is not verified, or the user has a customer profile with an unverified phone number.";
            s.Params[nameof(LoginCommand.Email)] = "User identifier: full email address, or exactly nine digits for phone login (no country prefix in the validator).";
            s.Params[nameof(LoginCommand.Password)] = "Current account password.";
        });
        Description(desc => desc
            .WithTags("Auth"));
    }

    public override async Task<Results<Ok<LoginResponse>, ProblemDetails>> ExecuteAsync(LoginCommand req, CancellationToken ct)
    {
        var result = await req.ExecuteAsync(ct);
        if (result.IsError)
        {
            return result.ToProblemDetails();
        }

        return TypedResults.Ok(new LoginResponse(result.Value));
    }
}