using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.PurchasingAndDistribution.BuyOrders;

public sealed class BuyOrder : Entity
{
    public Guid ContaMasterId { get; set; } // entender se esse negocio é daoende
    public string Ticker { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public MarketType Type { get; set; }
    public DateTime ExecutedAt { get; set; }
}
