using ErrorOr;

namespace CapitalFlow.Api.Features.Customers.Customers.UpdateAmount;

public record UpdateAmountCommand : ICommand<ErrorOr<Success>>
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
}