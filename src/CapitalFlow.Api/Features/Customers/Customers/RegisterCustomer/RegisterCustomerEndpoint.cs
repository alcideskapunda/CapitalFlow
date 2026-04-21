using CapitalFlow.Api.Features.Common.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.Customers.Customers.RegisterCustomer;

public sealed class RegisterCustomerEndpoint : Endpoint<RegisterCustomerCommand, Results<Created<RegisterCustomerResponse>, ProblemDetails>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("customers/subscription");
        Summary(s =>
        {
            s.Summary = "Register customer account";
            s.Description = "Upon joining, the system should automatically create a Puppy Graphic Account and a Puppy Custody account linked to the client..";
            s.Params[nameof(RegisterCustomerCommand.Name)] = "Customer display / billing name.";
            s.Params[nameof(RegisterCustomerCommand.Email)] = "Customer contact email (may mirror login email).";
            s.Params[nameof(RegisterCustomerCommand.Cpf)] = "Tax identification number (CPF) for the customer.";
            s.Params[nameof(RegisterCustomerCommand.Password)] = "Initial password for the new user (hashed before storage).";
        });
        Description(desc => desc
            .WithTags("Customers"));
    }

    public override async Task<Results<Created<RegisterCustomerResponse>, ProblemDetails>> ExecuteAsync(RegisterCustomerCommand req, CancellationToken ct)
    {
        var result = await req.ExecuteAsync(ct);

        if (result.IsError)
        {
            return result.ToProblemDetails();
        }

        return TypedResults.Created("", result.Value);
    }
}