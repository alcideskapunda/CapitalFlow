namespace CapitalFlow.Domain.Entities.Customers.Customers;

public interface ICustomerRepository
{
    Task<bool> CpfExistsAsync(string cpf, CancellationToken ct);
    bool MinimumAmountPolicy(decimal amount);
}