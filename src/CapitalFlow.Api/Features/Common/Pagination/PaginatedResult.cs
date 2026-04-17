namespace CapitalFlow.Api.Features.Common.Pagination;

public record PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; } = null!;
    public int ItemsCount { get; set; }
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public PaginatedResult(IEnumerable<T> items,
                           PaginationData paginationData)
    {
        Items = items;
        ItemsCount = items.Count();
        TotalItems = paginationData.TotalItems;
        Page = paginationData.Page;
        PageSize = paginationData.PageSize;
    }

}
