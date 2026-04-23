using CapitalFlow.Api.Features.Common.Pagination;
using CapitalFlow.Api.Features.RecommendationBasket.Common;

namespace CapitalFlow.Api.Features.RecommendationBasket.GetBasketHistories;

public record GetBasketHistoryQuery : PaginationQuery<BasketViewModel> {}