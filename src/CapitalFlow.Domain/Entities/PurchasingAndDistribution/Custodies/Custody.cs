using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Distributions;

namespace CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;

public sealed class Custody : Entity
{
    public Guid GraphicAccountId { get; set; }
    public string Ticker { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal AveragePrice { get; set; }
    public DateTime UpdatedAt { get; set; }
    public GraphicAccount? GraphicAccount { get; set; }
    public ICollection<Distribution>? Distributions { get; set; }
}
