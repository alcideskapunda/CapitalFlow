using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Api.Features.Common.Pagination;

public static class PaginationExtension
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationData paginationData)
    {
        return query.Skip((paginationData.Page - 1) * paginationData.PageSize).Take(paginationData.PageSize);
    }

    public static IQueryable<T> ApplyStartDateFilter<T>(this IQueryable<T> query, DateTime? startDate) where T : Entity
    {
        if (startDate.HasValue)
        {
            query = query.Where(x => x.CreatedAt >= startDate.Value);
        }
        return query;
    }

    public static IQueryable<T> ApplyEndDateFilter<T>(this IQueryable<T> query, DateTime? endDate) where T : Entity
    {
        if (endDate.HasValue)
        {
            query = query.Where(x => x.CreatedAt <= endDate.Value);
        }
        return query;
    }

}
