using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;
using CapitalFlow.Domain.Entities.Rebalancing.EventIRs;
using CapitalFlow.Domain.Entities.Rebalancing.RebalancingEvent;
using CapitalFlow.Domain.Entities.Users;

namespace CapitalFlow.Domain.Entities.Customers.Customers;

public sealed class Customer : Entity
{
    public string Name { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid UserId { get; set; }
    public decimal MonthlyAmount { get; set; }
    public bool Status { get; set; }
    public DateTime JoinDate { get; set; }
    public User? User { get; set; }
    public GraphicAccount? GraphicAccount { get; set; }
    public ICollection<EventIR>? EventIRs { get; set; }
    public ICollection<RebalancingEvent>? Rebalancings { get; set; }
}
