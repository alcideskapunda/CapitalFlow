using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Persistence.Database;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Customers.Customers.UpdateAmount;

public class UpdateAmountCommandHandler : ICommandHandler<UpdateAmountCommand, ErrorOr<Success>>
{
    private readonly CapitalFlowDbContext _database;
    private readonly ICustomerRepository _repository;
    private readonly ILogger<UpdateAmountCommandHandler> _logger;

    public UpdateAmountCommandHandler(CapitalFlowDbContext database, ICustomerRepository repository, ILogger<UpdateAmountCommandHandler> logger)
    {
        _database = database;
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Success>> ExecuteAsync(UpdateAmountCommand request, CancellationToken ct)
    {
        var customer = await _database.Customers.FirstOrDefaultAsync(c => c.Id == request.Id, ct);
        
        if (customer is null)
        {
            return Error.NotFound(code: "CLIENTE_NAO_ENCONTRADO", description: "Customer notfound");
        }

        if (!_repository.MinimumAmountPolicy(request.Amount))
        {
            return Error.Conflict(code: "VALOR_MENSAL_INVALIDO", description: "The minimum monthly amount is R$ 100.00.");
        }

        var lastAmount = await _database.CustomerContributionHistories
            .Where(x => x.CustomerId == request.Id)
            .OrderByDescending(x => x.StartDate)
            .Select(x => x.Amount)
            .FirstOrDefaultAsync(ct);

        if (lastAmount == request.Amount)
        {
            return Result.Success;
        }

        var customerHistory = CustomerContributionHistory.Create(
            customerId: request.Id,
            amount: request.Amount
        );
        
        _database.CustomerContributionHistories.Add(customerHistory.Value);
        
        customer.MonthlyAmount = request.Amount;
        customer.UpdatedAt = DateTime.UtcNow;

        await _database.SaveChangesAsync(ct);
        
        _logger.LogInformation("Updated Customer Contribution History for {CustomerId}", request.Id);

        return Result.Success;
    }
}