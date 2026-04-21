using ErrorOr;

namespace CapitalFlow.Api.Features.Customers.Customers.RegisterCustomer;

public record RegisterCustomerCommand : ICommand<ErrorOr<RegisterCustomerResponse>>
{
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal MonthlyAmount { get; set; }
    public string Password { get; set; } = null!;
};