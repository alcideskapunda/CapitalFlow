using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;

public sealed class RecommendationBasket : Entity
{
    public string Name { get; set; } = null!;
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeactivatedAt { get; set; }
}
