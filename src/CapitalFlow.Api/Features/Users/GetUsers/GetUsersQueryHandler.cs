using CapitalFlow.Api.Features.Common.Pagination;
using CapitalFlow.Api.Features.Users.Common;
using CapitalFlow.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Users.GetUsers;

public class GetUsersQueryHandler : ICommandHandler<GetUsersQuery, PaginatedResult<UserViewModel>>
{
    private readonly CapitalFlowDbContext _database;

    public GetUsersQueryHandler(CapitalFlowDbContext database)
    {
        _database = database;
    }

    public async Task<PaginatedResult<UserViewModel>> ExecuteAsync(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = _database.Users!
            .AsNoTracking()
            .ApplyStartDateFilter(query.StartDate)
            .ApplyEndDateFilter(query.EndDate);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            users = users.Where(d => d.Name.ToLower().Contains(query.Search.ToLower()));
        }

        if (query.Email != null)
        {
            users = users.Where(u => u.Email == query.Email);
        }

        var paginationData = await query.GetPaginationDataAsync(users, cancellationToken);

        var items = await users.OrderByDescending(d => d.CreatedAt)
            .Paginate(paginationData)
            .ProjectToViewModel()
            .ToListAsync(cancellationToken);

        return new PaginatedResult<UserViewModel>(items, paginationData);
    }
}
