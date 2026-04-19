using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.BuyOrders;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;
using ErrorOr;

namespace CapitalFlow.Domain.Entities.Customers.GraphicAccounts;

public sealed class GraphicAccount : Entity
{
    public Guid CustumerId { get; set; }
    public string AccountNumber { get; set; } = null!;
    public AccountType Type { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<Custody>? Custodies { get; set; }
    public ICollection<BuyOrder>? BuyOrders { get; set; }

    public static ErrorOr<GraphicAccount> Create(Guid customerId)
    {
        string accountNumber = GenerateAccountNumber(customerId);
        // account.Number = $"FLH-{customer.Id:D6}";
        return new GraphicAccount
        {
            CustumerId = customerId,
            AccountNumber = accountNumber,
            Type = AccountType.Filhote
        };
    }
    
    private static string GenerateAccountNumber(Guid customerId)
    {
        return $"FLH-{customerId.ToString("N")[..6].ToUpper()}";
    }
}
