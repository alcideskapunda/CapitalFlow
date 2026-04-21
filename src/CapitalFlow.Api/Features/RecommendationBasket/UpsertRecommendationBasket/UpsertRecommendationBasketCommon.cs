using ErrorOr;

namespace CapitalFlow.Api.Features.RecommendationBasket.UpsertRecommendationBasket;

public record UpsertRecommendationBasketCommon : ICommand<ErrorOr<UpsertRecommendationBasketResponse>>
{
    public string Name { get; set; } = null!;
    public ICollection<UpsertBasketItemDto> BasketItems { get; set; } = [];
}

public record UpsertBasketItemDto
{
    public string Ticker { get; set; } = null!;
    public decimal Percentage { get; set; }
}