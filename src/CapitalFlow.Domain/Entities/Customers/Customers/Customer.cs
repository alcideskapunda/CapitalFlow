using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;
using CapitalFlow.Domain.Entities.Rebalancing.EventIRs;
using CapitalFlow.Domain.Entities.Rebalancing.RebalancingEvent;
using CapitalFlow.Domain.Entities.Users;
using ErrorOr;

namespace CapitalFlow.Domain.Entities.Customers.Customers;

public sealed class Customer : Entity
{
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid UserId { get; set; }
    public decimal MonthlyAmount { get; set; }
    public bool Status { get; set; }
    public DateTime JoinDate { get; set; }
    public User? User { get; set; }
    public GraphicAccount? GraphicAccount { get; set; }
    public ICollection<EventIR>? EventIRs { get; set; }
    public ICollection<RebalancingEvent>? Rebalancings { get; set; }

    public static async Task<ErrorOr<Customer>> CreateAsync(Guid userId, string name, string cpf, string email, decimal monthlyAmount, ICustomerRepository customerRepository, CancellationToken ct)
    {
        if (await customerRepository.CpfExistsAsync(cpf, ct))
        {
            return Error.Conflict(code: "CLIENTE_CPF_DUPLICADO", description: "CPF already exists");
        }

        if (!customerRepository.MinimumAmountPolicy(monthlyAmount))
        {
            return Error.Conflict(code: "VALOR_MENSAL_INVALIDO", description: "The minimum monthly amount is R$ 100.00.");
        }

        return new Customer
        {
            Name = name,
            Cpf = cpf,
            Email = email,
            UserId = userId,
            MonthlyAmount = monthlyAmount,
            Status = true,
            JoinDate = DateTime.UtcNow,
        };
    }
}
