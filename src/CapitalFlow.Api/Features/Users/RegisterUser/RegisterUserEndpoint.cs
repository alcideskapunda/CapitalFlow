using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Features.Common.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Users.RegisterUser;

public sealed class RegisterUserEndpoint : Endpoint<RegisterUserCommand, Results<Created<CreateEntityResponse>, ProblemDetails>>
{
    public override void Configure()
    {
        Roles(AuthorizationRoles.Admin);
        Post("/users");
        Summary(s =>
        {
            s.Summary = "Create user";
            s.Description =
                "Creates a new user account in the CapitalFlow platform. This user can represent an investor or system operator and will be used for authentication, authorization, and access to financial operations within the system.";

            s.Params[nameof(RegisterUserCommand.Name)] = "Full name of the user.";
            s.Params[nameof(RegisterUserCommand.Email)] = "Unique email address used for authentication.";
            s.Params[nameof(RegisterUserCommand.Password)] = "User password (will be securely hashed before storage).";
            s.Params[nameof(RegisterUserCommand.Type)] =
                "User role within the system (e.g., Customer or Admin), which defines permissions and access levels.";
        });
        Description(desc => desc
            .WithTags("Users"));
    }

    public override async Task<Results<Created<CreateEntityResponse>, ProblemDetails>> ExecuteAsync(RegisterUserCommand command, CancellationToken ct)
    {
        var result = await command.ExecuteAsync(ct);
        if (result.IsError)
        {
            return result.ToProblemDetails();
        }

        var user = result.Value;

        return TypedResults.Created("", new CreateEntityResponse(user.Id));
    }
}
