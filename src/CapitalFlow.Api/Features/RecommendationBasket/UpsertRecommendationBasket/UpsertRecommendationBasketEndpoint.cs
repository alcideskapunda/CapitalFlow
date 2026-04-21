using CapitalFlow.Api.Authorization;
using CapitalFlow.Api.Features.Common.Requests;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CapitalFlow.Api.Features.RecommendationBasket.UpsertRecommendationBasket;

public sealed class UpsertRecommendationBasketEndpoint : Endpoint<UpsertRecommendationBasketCommon, Results<Created<UpsertRecommendationBasketResponse>, ProblemDetails>>
{
    public override void Configure()
    {
        Roles(AuthorizationRoles.Admin);
        Post("admin/recommendation-baskets");
        Summary(s =>
        {
            s.Summary = "Upserts a recommendation basket";
            s.Description = "Upserts a recommendation basket";
        });
        Description(desc => desc
            .WithTags("Admin"));
    }

    public override async Task<Results<Created<UpsertRecommendationBasketResponse>, ProblemDetails>> ExecuteAsync(UpsertRecommendationBasketCommon req, CancellationToken ct)
    {
        var result = await req.ExecuteAsync(ct);
        if (result.IsError)
        {
            return result.ToProblemDetails();
        }
        // evento de rebalenciamento apos alterar as cestas.
        return TypedResults.Created("", result.Value);
    }
}