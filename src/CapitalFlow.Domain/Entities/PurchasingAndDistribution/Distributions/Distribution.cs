using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.BuyOrders;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;

namespace CapitalFlow.Domain.Entities.PurchasingAndDistribution.Distributions;

public sealed class Distribution : Entity
{
    public Guid BuyOrderId { get; set; }
    public Guid CustodyFilhoteId { get; set; }
    public string Ticker { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime DistributedAt { get; set; } = DateTime.UtcNow;
    public BuyOrder? BuyOrder { get; set; }
    public Custody? Custodies { get; set; }
}
