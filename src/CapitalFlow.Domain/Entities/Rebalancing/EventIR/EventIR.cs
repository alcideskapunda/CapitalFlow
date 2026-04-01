using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Rebalancing.EventIR;

public sealed class EventIR : Entity
{
    public Guid CustumerId { get; set; } // fk. depois fazer relacionamento
    public decimal BaseAmount { get; set; }
    public decimal IRAmount { get; set; }
    public bool IsPublishedToKafka { get; set; }
    public DateTime EventDate { get; set; }
}
