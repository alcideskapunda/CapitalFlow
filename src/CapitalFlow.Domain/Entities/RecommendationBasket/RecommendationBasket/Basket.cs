using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.RecommendationBasket.BasketItems;

namespace CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;

public sealed class Basket : Entity
{
    public string Name { get; set; } = null!;
    public bool Status { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    public ICollection<BasketItem>? BasketItems { get; set; }
}
