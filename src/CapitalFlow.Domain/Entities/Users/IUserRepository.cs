using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Users;

public interface IUserRepository : IRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
}
