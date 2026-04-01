using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Quotes;

public sealed class Quote : Entity
{
    public DateTime TradeDate { get; set; } = DateTime.UtcNow;
    public string Ticker { get; set; } = null!;
    public decimal OpenPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
}
