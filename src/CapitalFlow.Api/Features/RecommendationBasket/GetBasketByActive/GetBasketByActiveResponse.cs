namespace CapitalFlow.Api.Features.RecommendationBasket.GetBasketByActive;

public record ActiveBasketViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? DeactivatedAt { get; init; }
    public IEnumerable<AtiveBasketItems>? BasketItems { get; init; }
}

public record AtiveBasketItems
{
    public string Ticker { get; init; } = null!;
    public decimal Percentage { get; init; }
    public decimal CurrentQuote { get; init; }
}