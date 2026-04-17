namespace CapitalFlow.Api.Features.Common.Pagination;

public sealed record PaginationData
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int Page { get; set; }
}
