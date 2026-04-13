namespace CapitalFlow.Domain.Entities.Common;

public interface IRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
}
