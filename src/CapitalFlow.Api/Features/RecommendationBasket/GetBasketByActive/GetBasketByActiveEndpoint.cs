using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Features.Common.Requests;
using CapitalFlow.Api.Features.RecommendationBasket.Common;
using CapitalFlow.Persistence.Database;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.RecommendationBasket.GetBasketByActive;

public sealed class GetBasketByActiveEndpoint : EndpointWithoutRequest<Results<Ok<GetBasketByActiveResponse>, ProblemDetails>>
{
    private readonly CapitalFlowDbContext _database;

    public GetBasketByActiveEndpoint(CapitalFlowDbContext database)
    {
        _database = database;
    }

    public override void Configure()
    {
        Roles(AuthorizationRoles.Admin);
        Get("admin/recommendation-baskets/current");
        Summary(s =>
        {
            s.Summary = "Get active basket";
            s.Description = "Returns the currently active recommendation basket.";
        });
        Description(desc => desc
            .WithTags("Admin"));
    }

    public override async Task<Results<Ok<GetBasketByActiveResponse>, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        var basket = await _database.Baskets!
            .AsNoTracking()
            .Where(x => x.Status)
            .ToBasketViewModel()
            .FirstOrDefaultAsync(ct);
        
        var tickers = basket!.BasketItems!.Select(x => x.Ticker).Distinct().ToList();

        var quotes = await _database.B3StockQuotes
            .AsNoTracking()
            .Where(x => tickers.Contains(x.Ticker))
            .GroupBy(x => x.Ticker)
            .Select(x => new
            {
                Ticker = x.Key,
                LastPrice = x.OrderByDescending(x => x.TradingDate)
                    .Select(x => x.ClosePrice)
                    .FirstOrDefault(),
            }).ToDictionaryAsync(x => x.Ticker, x => x.LastPrice, StringComparer.OrdinalIgnoreCase, ct);

        var response = new GetBasketByActiveResponse
        {
            Id = basket.Id,
            Name = basket.Name,
            Status = basket.Status,
            CreatedAt = basket.CreatedAt,
            BasketItems = basket.BasketItems!.Select(x => new BasketItems
            {
                Ticker = x.Ticker,
                Percentage =  x.Percentage,
                CurrentQuote = quotes.TryGetValue(x.Ticker, out var price) ? price : 0m 
            }).ToList(),
        };

        return TypedResults.Ok(response);       
    }
}