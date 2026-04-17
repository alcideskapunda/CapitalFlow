using CapitalFlow.Api.Features.Users.Common;
using CapitalFlow.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Users.GetUserById;

public class GetUserByIdQueryHandler : ICommandHandler<GetUserByIdQuery, UserViewModel?>
{
    private readonly CapitalFlowDbContext _database;

    public GetUserByIdQueryHandler(CapitalFlowDbContext database)
    {
        _database = database;
    }

    public async Task<UserViewModel?> ExecuteAsync(GetUserByIdQuery command, CancellationToken ct)
    {
        var user = await _database.Users!
            .Where(u => u.Id == command.Id)
            .ProjectToViewModel()
            .FirstOrDefaultAsync(ct);

        return user;
    }
}
