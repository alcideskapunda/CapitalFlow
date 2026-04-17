using CapitalFlow.Api.Features.Users.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Users.GetUserById;

public sealed class GetUserByIdEndpoint : Endpoint<GetUserByIdQuery, Results<Ok<UserViewModel>, NotFound>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/users/{Id:guid}");
        Summary(s =>
        {
            s.Summary = "Get user by identifier";
            s.Description =
                "Returns the public user profile for the given id. Responds with 404 when no user exists with that identifier.";
            s.Params[nameof(GetUserByIdQuery.Id)] =
                "Unique user id (GUID) as assigned at registration.";
        });
        Description(desc => desc
            .WithTags("Users"));
    }

    public override async Task<Results<Ok<UserViewModel>, NotFound>> ExecuteAsync(GetUserByIdQuery query, CancellationToken ct)
    {
        var result = await query.ExecuteAsync(ct);
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }
}
