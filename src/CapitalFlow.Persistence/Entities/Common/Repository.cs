using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Persistence.Entities.Common;

public abstract class Repository<TEntity> : IRepository where TEntity : Entity
{
    protected readonly CapitalFlowDbContext Database;

    public Repository(CapitalFlowDbContext database)
    {
        Database = database;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await Database.Set<TEntity>().AnyAsync(e => e.Id == id, ct);
    }
}
