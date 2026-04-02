using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;

namespace CapitalFlow.Domain.Entities.RecommendationBasket.BasketItems;

public class BasketItem : Entity
{
    public Guid BasketId { get; set; }
    public string Ticker { get; set; } = null!;
    public decimal Percentage { get; set; }
    public Basket? Basket { get; set; }
}
