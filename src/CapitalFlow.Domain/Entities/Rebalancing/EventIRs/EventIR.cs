using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.Customers;

namespace CapitalFlow.Domain.Entities.Rebalancing.EventIRs;

public sealed class EventIR : Entity
{
    public Guid CustomerId { get; set; }
    public decimal BaseAmount { get; set; }
    public decimal IRAmount { get; set; }
    public bool IsPublishedToKafka { get; set; }
    public DateTime EventDate { get; set; }
    public Customer? Customer { get; set; }
}
