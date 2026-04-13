using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Database;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Persistence.Entities.Users.Users;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CapitalFlowDbContext database) : base(database) { }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        return await Database.Users.AnyAsync(u => u.Email == email, ct);
    }
}
