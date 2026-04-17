namespace CapitalFlow.Api.Features.Common.Pagination;

/// <summary>
/// Shared OpenAPI / Scalar parameter descriptions for <see cref="PaginationQuery{TEntity}"/>.
/// </summary>
internal static class PaginationQuerySummaryExtensions
{
    public static void AddPaginationQueryParams<T, TEntity>(this EndpointSummary<T> s)
        where T : PaginationQuery<TEntity>
    {
        s.RequestParam(r => r.Page, "Page number (1-based). Omitted or out of range is clamped to a valid page.");
        s.RequestParam(r => r.PageSize, "Items per page; must be at least 1 when provided. Omitted uses a server default.");
        s.RequestParam(r => r.StartDate, "Optional inclusive lower bound for date-based filters (documents, movements, etc.).");
        s.RequestParam(r => r.EndDate, "Optional inclusive upper bound for date-based filters.");
        s.RequestParam(r => r.Search, "Optional text applied to designated searchable fields on the server.");
    }
}
