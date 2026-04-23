using CapitalFlow.Api.Features.Common.Pagination;
using CapitalFlow.Api.Features.RecommendationBasket.Common;
using CapitalFlow.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.RecommendationBasket.GetBasketHistories;

public sealed class GetBasketHistoryQueryHandler : ICommandHandler<GetBasketHistoryQuery, PaginatedResult<BasketViewModel>>
{
    private readonly CapitalFlowDbContext _database;
    private readonly ILogger<GetBasketHistoryQueryHandler> _logger;

    public GetBasketHistoryQueryHandler(CapitalFlowDbContext database, ILogger<GetBasketHistoryQueryHandler> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<PaginatedResult<BasketViewModel>> ExecuteAsync(GetBasketHistoryQuery query, CancellationToken ct)
    {
        var baskets = _database.Baskets!
            .AsNoTracking()
            .ApplyStartDateFilter(query.StartDate)
            .ApplyEndDateFilter(query.EndDate);
        
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            baskets = baskets.Where(x => x.Name.ToLower().Contains(query.Search.ToLower()));
        }

        var paginationData = await query.GetPaginationDataAsync(baskets, ct);

        var items = await baskets.OrderByDescending(x => x.CreatedAt)
            .Paginate(paginationData)
            .ToBasketViewModel()
            .ToListAsync(ct);
        
        _logger.LogInformation("History successfully returned. Items on the page: {Count}", items.Count);
        
        return new PaginatedResult<BasketViewModel>(items, paginationData);
    }
}