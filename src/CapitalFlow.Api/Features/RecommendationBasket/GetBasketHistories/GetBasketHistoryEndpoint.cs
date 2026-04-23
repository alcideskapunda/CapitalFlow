using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Features.Common.Pagination;
using CapitalFlow.Api.Features.RecommendationBasket.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.RecommendationBasket.GetBasketHistories;

public class GetBasketHistoryEndpoint : Endpoint<GetBasketHistoryQuery, Results<Ok<PaginatedResult<BasketViewModel>>, ProblemDetails>>
{
    public override void Configure()
    {
        Roles(AuthorizationRoles.Admin);
        Get("admin/recommendation-baskets/history");
        Summary(s =>
        {
            s.Summary = "Get recommendation baskets history";
            s.Description = "Retrieves a paginated list of basket histories with support for date range filtering and name-based searching.";
        });
        Description(desc => desc
            .WithTags("Admin"));
    }

    public override async Task<Results<Ok<PaginatedResult<BasketViewModel>>, ProblemDetails>> ExecuteAsync(GetBasketHistoryQuery query, CancellationToken ct)
    {
        var result = await query.ExecuteAsync(ct);
        return TypedResults.Ok(result);
    }
}