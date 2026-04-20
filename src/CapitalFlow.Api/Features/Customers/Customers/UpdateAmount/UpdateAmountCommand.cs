namespace CapitalFlow.Api.Features.Customers.Customers.UpdateAmount;

public record UpdateAmount
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
}