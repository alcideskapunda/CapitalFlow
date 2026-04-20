using CapitalFlow.Persistence.Database;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Customers.Customers.CancelSubscription;

public sealed class CancelSubscriptionCommandHandler : ICommandHandler<CancelSubscriptionCommand, ErrorOr<Success>>
{
    private readonly CapitalFlowDbContext _database;
    private readonly ILogger<CancelSubscriptionCommandHandler> _logger;

    public CancelSubscriptionCommandHandler(CapitalFlowDbContext database, ILogger<CancelSubscriptionCommandHandler> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<ErrorOr<Success>> ExecuteAsync(CancelSubscriptionCommand request, CancellationToken ct)
    {
        var customer = await _database.Customers.FirstOrDefaultAsync(c => c.Id == request.Id, ct);
        
        if (customer is null)
        {
            return Error.NotFound(code: "CLIENTE_NAO_ENCONTRADO", description: "Customer notfound");
        }
        
        if (!customer.Status)
        {
            return Error.Conflict("CUSTOMER_ALREADY_INACTIVE", "Customer already unsubscribed");
        }

        customer.Status = false;
        customer.UpdatedAt = DateTime.UtcNow;

        await _database.SaveChangesAsync(ct);
        
        _logger.LogInformation("Customer {CustomerId} cancelled subscription", request.Id);

        return ErrorOr.Result.Success;
    }
}