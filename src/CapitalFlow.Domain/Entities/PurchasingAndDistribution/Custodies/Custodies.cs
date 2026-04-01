using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;

public sealed class Custodies : Entity
{
    public Guid GraphicAccountId { get; set; } // relacionameto
    public string Ticker { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal AveragePrice { get; set; }
    public DateTime UpdatedAt { get; set; }
}
