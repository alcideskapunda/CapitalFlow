using CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;

namespace CapitalFlow.Api.Features.RecommendationBasket.Common;

public static class BasketViewModelExtension
{
    public static IQueryable<BasketViewModel> ToBasketViewModel(this IQueryable<Basket> baskets)
    {
        return baskets.Select(x => new BasketViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
            DeactivatedAt = x.DeactivatedAt,
            BasketItems = x.BasketItems!.Select(i => new BasketItems
            {
                Ticker = i.Ticker,
                Percentage = i.Percentage,
            })
        });
    }
}