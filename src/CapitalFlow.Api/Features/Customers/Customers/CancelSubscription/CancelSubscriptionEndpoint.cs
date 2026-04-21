using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Features.Common.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Customers.Customers.CancelSubscription;

public sealed class CancelSubscriptionEndpoint : EndpointWithoutRequest<Results<Ok, ProblemDetails>>
{
    public override void Configure()
    {
        Roles(AuthorizationRoles.Admin, AuthorizationRoles.Customer);
        Patch("customers/{id:guid}/exit");
        Summary(s =>
        {
            s.Summary = "Cancel customer subscription";
            s.Description =
                "Cancels the customer's subscription, preventing future investment executions while preserving all existing assets and custody positions.";
        });
        Description(desc => desc
            .WithTags("Customers"));
    }

    public override async Task<Results<Ok, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var command = new CancelSubscriptionCommand { Id = id };
        var result = await command.ExecuteAsync(ct);
        if (result.IsError)
        {
            return result.ToProblemDetails();
        }

        return TypedResults.Ok();
    }
}