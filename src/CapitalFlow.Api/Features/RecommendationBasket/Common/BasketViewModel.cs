namespace CapitalFlow.Api.Features.RecommendationBasket.Common;

public record BasketViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? DeactivatedAt { get; init; }
    public IEnumerable<BasketItems>? BasketItems { get; init; }
}

public record BasketItems
{
    public string Ticker { get; init; } = null!;
    public decimal Percentage { get; init; }
}

