using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CapitalFlow.Persistence.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    private readonly IAuditingInformationService _auditingInformationService;

    public AuditingInterceptor(IAuditingInformationService auditingInformationService)
    {
        _auditingInformationService = auditingInformationService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach (var entry in eventData.Context!.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedBy").CurrentValue = _auditingInformationService.GetUserId() ?? entry.Property("CreatedBy").CurrentValue;
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Property("UpdatedBy").CurrentValue = _auditingInformationService.GetUserId() ?? entry.Property("UpdatedBy").CurrentValue;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
