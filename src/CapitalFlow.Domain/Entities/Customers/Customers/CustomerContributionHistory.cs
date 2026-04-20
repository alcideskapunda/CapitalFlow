using CapitalFlow.Domain.Entities.Common;
using ErrorOr;

namespace CapitalFlow.Domain.Entities.Customers.Customers;

public sealed class CustomerContributionHistory : Entity
{
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public Customer? Customer { get; set; }

    public static ErrorOr<CustomerContributionHistory> Create(Guid customerId, decimal amount)
    {
        return new CustomerContributionHistory
        {
            CustomerId = customerId,
            Amount = amount,
            StartDate = DateTime.UtcNow,
        };
    }
}