using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.PurchasingAndDistribution.Distributions;

public sealed class Distribuition : Entity
{
    public Guid BuyOrderId { get; set; } //
    public Guid CustodieFilhoteId { get; set; } // / entender se esse negocio é daoende
    public string Ticker { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime DistributedAt { get; set; }
}
