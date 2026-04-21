using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapitalFlow.Api.Features.Customers.Customers.UpdateAmount;

public record UpdateAmountCommand : ICommand<ErrorOr<Success>>
{
    [FromRoute] public Guid Id { get; set; }
    public decimal Amount { get; set; }
}