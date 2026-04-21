namespace CapitalFlow.Api.Features.RecommendationBasket.UpsertRecommendationBasket;

public record UpsertRecommendationBasketResponse
{
    public Guid BasketId { get; set; }
    public string Name { get; set; } = null!;
    public bool Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<BasketItemResponse> BasketItems { get; init; } = [];

    public bool IsRebalancing { get; init; }
    public List<string>? RemovedAssets { get; init; }
    public List<string>? AddedAssets { get; init; }

    public PreviousBasketResponse? PreviousBasketDisabled { get; init; }
    public string Message { get; init; } = null!;
}

public record BasketItemResponse
{
    public string Ticker { get; init; } = null!;
    public decimal Percentage { get; init; }
}

public record PreviousBasketResponse
{
    public Guid BasketId { get; init; }
    public string Name { get; set; } = null!;
    public DateTime? DeactivatedAt { get; set; }
}