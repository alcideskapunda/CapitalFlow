using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.BuyOrders;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;

namespace CapitalFlow.Domain.Entities.Customers.GraphicAccounts;

public sealed class GraphicAccount : Entity
{
    public Guid CustumerId { get; set; }
    public string AccountNumber { get; set; } = null!;
    public AccountType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Customer? Customer { get; set; }
    public ICollection<Custody>? Custodies { get; set; }
    public ICollection<BuyOrder>? BuyOrders { get; set; }
}
