using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Customers.Customers;

public sealed class Customer : Entity
{
    public string Name { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal MonthlyAmount { get; set; }
    public bool Status { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
}
