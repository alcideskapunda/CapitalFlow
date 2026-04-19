using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Persistence.Database;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Persistence.Entities.Customers.Customers;

public sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(CapitalFlowDbContext database) : base(database) { }

    public async Task<bool> CpfExistsAsync(string cpf, CancellationToken ct)
    {
        return await Database.Customers.AnyAsync(c => c.Cpf == cpf, ct);
    }

    public bool MinimumAmountPolicy(decimal amount)
    {
        const decimal minimumAmountPolicy = 100.00m;
        return amount >= minimumAmountPolicy;
    }
}