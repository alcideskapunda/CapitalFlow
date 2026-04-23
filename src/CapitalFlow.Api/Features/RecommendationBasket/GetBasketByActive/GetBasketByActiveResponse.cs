namespace CapitalFlow.Api.Features.RecommendationBasket.GetBasketByActive;

public record GetBasketByActiveResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public IEnumerable<BasketItems>? BasketItems { get; init; }
}

public record BasketItems
{
    public string Ticker { get; init; } = null!;
    public decimal Percentage { get; init; }
    public decimal CurrentQuote { get; init; }
}