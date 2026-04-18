using CapitalFlow.Api.Features.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Common.Pagination;

public abstract record PaginationQuery<TEntity> : IQuery<PaginatedResult<TEntity>>
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Search { get; set; }

    public async Task<PaginationData> GetPaginationDataAsync<T>(IQueryable<T> query, CancellationToken ct)
    {
        var totalItems = await query.CountAsync(ct);
        var pageSize = Math.Max(PageSize ?? 10, 1);
        var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
        if (totalPages == 0)
        {
            totalPages = 1;
        }
        var page = Math.Min(Page ?? 1, totalPages);

        return new PaginationData
        {
            TotalItems = totalItems,
            PageSize = pageSize,
            Page = page,
            TotalPages = totalPages,
        };
    }
}
