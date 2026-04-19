using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;

namespace CapitalFlow.Api.Features.Customers.Customers.RegisterCustomer;

public record RegisterCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid UserId { get; set; }
    public decimal MonthlyAmount { get; set; }
    public bool Status { get; set; }
    public DateTime JoinDate { get; set; }
    
    public GraphicAccountResponse? GraphicAccount { get; set; }
}

public record GraphicAccountResponse
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public AccountType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}