using CapitalFlow.Application.Features.Quotes;
using CapitalFlow.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CapitalFlow.Infrastructure.Features.Quotes;

public sealed class B3QuoteIngestionService
{
    private readonly IB3QuoteParser _parser;
    private readonly CapitalFlowDbContext _database;
    private readonly ILogger<B3QuoteIngestionService> _logger;

    public B3QuoteIngestionService(IB3QuoteParser parser, CapitalFlowDbContext database, ILogger<B3QuoteIngestionService> logger)
    {
        _parser = parser;
        _database = database;
        _logger = logger;
    }

    public async Task ProcessFileAsync(string pathFile, CancellationToken ct = default)
    {
        _logger.LogInformation("Processing file {PathFile} of B3", pathFile);

        if (!File.Exists(pathFile))
        {
            _logger.LogWarning("File {PathFile} not found", pathFile);
            return;
        }
        
        var quotes = _parser.ParseArchive(pathFile).ToList();

        if (quotes.Count == 0)
        {
            _logger.LogWarning("No quotes found for file {PathFile}", pathFile);
            return;
        }
        
        var fileDates = quotes.Select(q => q.TradingDate.Date).Distinct().ToList();
        
        var existingDates = await _database.B3StockQuotes
            .Where(q => fileDates.Contains(q.TradingDate.Date))
            .Select(q => q.TradingDate.Date)
            .Distinct()
            .ToListAsync(ct);
        
        if (existingDates.Count == fileDates.Count)
        {
            _logger.LogWarning("The prices for the dates in this file have already been entered. Operation safely aborted.");
            return;
        }
        
        var quotesToInsert = quotes
            .Where(q => !existingDates.Contains(q.TradingDate.Date))
            .ToList();

        if (quotesToInsert.Count == 0)
        {
            _logger.LogWarning("All the quotes in this file already existed in the bank.");
            return;
        }
        
        await  _database.B3StockQuotes.AddRangeAsync(quotesToInsert, ct);
        await _database.SaveChangesAsync(ct);
        
        _logger.LogInformation("Finished processing file {PathFile} of B3", pathFile);
    }
}