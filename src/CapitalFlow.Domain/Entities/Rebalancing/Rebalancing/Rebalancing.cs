using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Rebalancing.Rebalancing;

public sealed class Rebalancing : Entity
{
    public Guid CustumerId { get; set; } // fk. depois fazer relacionamento
    public RebalancingType Type { get; set; }
    public string SoldTicker { get; set; } = null!;
    public string BoughtTicker { get; set; } = null!;
    public decimal SaleAmount { get; set; }
    public DateTime RebalancingDate { get; set; } = DateTime.UtcNow;
}
