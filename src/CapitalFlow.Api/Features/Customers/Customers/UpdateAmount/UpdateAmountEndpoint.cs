using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Features.Common.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Customers.Customers.UpdateAmount;

public class UpdateAmountEndpoint : Endpoint<UpdateAmountCommand, Results<Ok, ProblemDetails>>
{
    public override void Configure()
    {
        Roles(AuthorizationRoles.Customer);
        Patch("customers/{id:guid}/monthly-value");
        Summary(s =>
        {
            s.Summary = "Update customer's monthly contribution amount";
            s.Description =
                "Updates the monthly investment amount for a customer. The change does not affect past purchase cycles and will only be considered in the next scheduled execution. " +
                "All previous contribution values are preserved in history to ensure traceability and accurate financial calculations.";
        });
        Description(desc => desc
            .WithTags("Customers"));
    }

    public override async Task<Results<Ok, ProblemDetails>> ExecuteAsync(UpdateAmountCommand req, CancellationToken ct)
    {
        var result = await req.ExecuteAsync(ct);
        if (result.IsError)
        {
            return result.ToProblemDetails();
        }

        return TypedResults.Ok();
    }
}