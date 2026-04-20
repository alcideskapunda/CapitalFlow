using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapitalFlow.Api.Features.Customers.Customers.CancelSubscription;

public record CancelSubscriptionCommand : ICommand<ErrorOr<Success>>
{
    [FromRoute] public Guid Id { get; set; }
}