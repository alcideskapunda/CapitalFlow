using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Customers.GraphicAccounts;

public sealed class GraphicAccount : Entity
{
    public Guid CustumerId { get; set; } // fk. depois fazer relacionamento
    public string AccountNumber { get; set; } = null!;
    public AccountType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
