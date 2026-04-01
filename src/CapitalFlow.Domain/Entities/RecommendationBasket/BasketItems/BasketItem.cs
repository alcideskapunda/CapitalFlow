using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.RecommendationBasket.BasketItems;

public class BasketItem : Entity
{
    public Guid BasketId { get; set; } // depois fazer relaçionamento com a recomendation baskect
    public string Ticker { get; set; } = null!;
    public decimal Percentage { get; set; }
}
