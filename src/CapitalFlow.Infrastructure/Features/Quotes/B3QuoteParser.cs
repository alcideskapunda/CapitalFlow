using System.Globalization;
using System.Text;
using CapitalFlow.Application.Features.Quotes;
using CapitalFlow.Domain.Entities.Quotes;

namespace CapitalFlow.Infrastructure.Features.Quotes;

public sealed class B3QuoteParser : IB3QuoteParser
{
    /// <summary>
    /// Le e faz parse de um arquivo COTAHIST da B3.
    /// Retorna apenas registros de detalhe (TIPREG = 01)
    /// filtrados por mercado a vista (010) e fracionario (020).
    /// </summary>
    public IEnumerable<B3StockQuote> ParseArchive(string pathFile)
    {
        var cotacoes = new List<B3StockQuote>();

        if (!File.Exists(pathFile))
        {
            return cotacoes;
        }
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding("ISO-8859-1");

        foreach (var line in File.ReadLines(pathFile, encoding))
        {
            if (line.Length < 245) continue;
            
            var recordType = line.Substring(0, 2);
            if(recordType != "01") continue;
            
            var typeMarket = int.Parse(line.Substring(24, 3).Trim());
            
            if (typeMarket != 10 && typeMarket != 20) continue;
            
            var cotacao = new B3StockQuote
            {
                TradingDate = DateTime.ParseExact(line.Substring(2, 8), "yyyyMMdd", CultureInfo.InvariantCulture),
                BdiCode = line.Substring(10, 2).Trim(),
                Ticker = line.Substring(12, 12).Trim(),
                MarketType = typeMarket,
                CompanyName = line.Substring(27, 12).Trim(),
                OpenPrice = ParsePrice(line.Substring(56, 13)),
                HighPrice = ParsePrice(line.Substring(69, 13)),
                LowPrice = ParsePrice(line.Substring(82, 13)),
                AveragePrice = ParsePrice(line.Substring(95, 13)),
                ClosePrice = ParsePrice(line.Substring(108, 13)),
                TradedQuantity = long.Parse(line.Substring(152, 18).Trim()),
                TradedVolume = ParsePrice(line.Substring(170, 18))
            };
            
            cotacoes.Add(cotacao);
        }
        
        return cotacoes;
    }
    
    /// <summary>
    /// Converte o valor inteiro do arquivo para decimal com 2 casas.
    /// Ex: "0000000003850" => 38.50m
    /// </summary>
    private decimal ParsePrice(string grossValue)
    {
        if (long.TryParse((grossValue.Trim()), out var price))
        {
            return price / 100m;
        }
        return 0m;
    }
}