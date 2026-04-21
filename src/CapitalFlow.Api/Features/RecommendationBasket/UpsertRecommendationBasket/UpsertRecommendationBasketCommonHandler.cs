using CapitalFlow.Domain.Entities.RecommendationBasket.BasketItems;
using CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;
using CapitalFlow.Persistence.Database;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.RecommendationBasket.UpsertRecommendationBasket;

public sealed class UpsertRecommendationBasketCommonHandler : ICommandHandler<UpsertRecommendationBasketCommon, ErrorOr<UpsertRecommendationBasketResponse>>
{
    private readonly CapitalFlowDbContext _database;
    private readonly ILogger<UpsertRecommendationBasketCommonHandler> _logger;

    public UpsertRecommendationBasketCommonHandler(CapitalFlowDbContext database, ILogger<UpsertRecommendationBasketCommonHandler> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<ErrorOr<UpsertRecommendationBasketResponse>> ExecuteAsync(UpsertRecommendationBasketCommon request, CancellationToken ct)
    {
        var executionStrategy = _database.Database.CreateExecutionStrategy();
        
        var transactionResult = await executionStrategy.ExecuteAsync(async () => await UpsetBasketAsync(request, ct));

        return transactionResult;
    }
    
    private async Task<ErrorOr<UpsertRecommendationBasketResponse>> UpsetBasketAsync(UpsertRecommendationBasketCommon request, CancellationToken ct) {
        _logger.LogInformation("Processing recommendation basket update: {BasketName}", request.Name);

        if (request.BasketItems.Count != 5)
        {
            _logger.LogWarning($"The basket must contain exactly 5 assets. Quantity specified: {request.BasketItems.Count}");
            return Error.Validation(code: "QUANTIDADE_ATIVOS_INVALIDA", description: $"The basket must contain exactly 5 assets. Quantity specified: {request.BasketItems.Count}");
        }

        var totalPercentage = request.BasketItems.Sum(x => x.Percentage);
        
        if (Math.Abs(totalPercentage - 100m) > 0.001m)
        {
            _logger.LogWarning($"The sum of the percentages must be exactly 100%. Current sum: {totalPercentage}%.");
            return Error.Validation(code: "PERCENTUAIS_INVALIDOS", description: $"The sum of the percentages must be exactly 100%. Current sum: {totalPercentage}%.");
        }

        if (request.BasketItems.Any(x => x.Percentage <= 0))
        {
            _logger.LogWarning("The percentage must be greater than 0.");
            return Error.Validation("PERCENTUAL_INVALIDO", "The percentage must be greater than 0.");
        }

        var hasDuplicate = request.BasketItems.GroupBy(x => x.Ticker.ToUpper().Trim()).Any(x => x.Count() > 1);
        
        if (hasDuplicate)
        {
            _logger.LogWarning("There cannot be duplicate tickets.");
            return Error.Validation("TICKER_DUPLICADO", "There cannot be duplicate tickets.");
        }

        await using var transaction = await _database.Database.BeginTransactionAsync(ct);

        var currentBasket = await _database.Baskets.Include(x => x.BasketItems).FirstOrDefaultAsync(x => x.Status, ct);
        
        List<string>? removedAssets = null;
        List<string>? addedAssets = null;
        PreviousBasketResponse? previousBasket = null;
        bool isRebalancing = false;

        if (currentBasket != null)
        {
            _logger.LogInformation("Deactivating previous basket: {BasketId} - {BasketName}. Rebalancing triggered.", currentBasket.Id, currentBasket.Name);

            currentBasket.Status = false;
            currentBasket.DeactivatedAt = DateTime.Now;

            previousBasket = new PreviousBasketResponse
            {
                BasketId = currentBasket.Id,
                Name = currentBasket.Name,
                DeactivatedAt = currentBasket.DeactivatedAt.Value
            };
            
            var oldTickers = currentBasket.BasketItems!.Select(x => x.Ticker).ToHashSet();
            var newTickers = request.BasketItems.Select(x => x.Ticker).ToHashSet();
            
            removedAssets = oldTickers.Except(newTickers).ToList();
            addedAssets = newTickers.Except(oldTickers).ToList();
            
            isRebalancing = true;
        }

        var basket = new Basket
        {
            Name = request.Name,
            Status = true,
            BasketItems = request.BasketItems.Select(x => new BasketItem
            {
                Ticker = x.Ticker.ToUpper().Trim(),
                Percentage =  x.Percentage,
            }).ToList()
        };

        await _database.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        var response = new UpsertRecommendationBasketResponse
        {
            BasketId = basket.Id,
            Name = basket.Name,
            Status = basket.Status,
            CreatedAt = basket.CreatedAt,
            BasketItems = basket.BasketItems.Select(x => new BasketItemResponse
            {
                Ticker = x.Ticker.ToUpper().Trim(),
                Percentage = x.Percentage
            }).ToList(),
            IsRebalancing = isRebalancing,
            RemovedAssets = removedAssets,
            AddedAssets = addedAssets,
            PreviousBasketDisabled = previousBasket,
            Message = isRebalancing ? "Basket updated. Rebalancing triggered." : "First basket successfully registered."
        };
        
        _logger.LogInformation("New basket {BasketId} successfully activated and persisted.", basket.Id);
        
        return response;
    }
}