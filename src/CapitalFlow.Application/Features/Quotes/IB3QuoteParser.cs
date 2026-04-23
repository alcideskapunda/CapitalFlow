using CapitalFlow.Domain.Entities.Quotes;

namespace CapitalFlow.Application.Features.Quotes;

public interface IB3QuoteParser
{
    /// <summary>
    /// Faz o parse do ficheiro TXT e devolve as cotações filtradas
    /// </summary>
    IEnumerable<B3StockQuote> ParseArchive(string pathFile);
}