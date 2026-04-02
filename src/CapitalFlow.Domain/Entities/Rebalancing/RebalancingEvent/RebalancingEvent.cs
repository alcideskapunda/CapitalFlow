using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.Customers;

namespace CapitalFlow.Domain.Entities.Rebalancing.RebalancingEvent;

public sealed class RebalancingEvent : Entity
{
    public Guid CustomerId { get; set; }
    public RebalancingType Type { get; set; }
    public string SoldTicker { get; set; } = null!;
    public string BoughtTicker { get; set; } = null!;
    public decimal SaleAmount { get; set; }
    public DateTime RebalancingEventDate { get; set; } = DateTime.UtcNow;
    public Customer? Customer { get; set; }
}
