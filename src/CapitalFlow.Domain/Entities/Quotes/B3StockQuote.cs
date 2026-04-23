using CapitalFlow.Domain.Entities.Common;

namespace CapitalFlow.Domain.Entities.Quotes;

public sealed class B3StockQuote : Entity
{
    public DateTime TradingDate { get; set; }           
    public string Ticker { get; set; } = string.Empty;  
    public string BdiCode { get; set; } = string.Empty; 
    public int MarketType { get; set; }                 
    public string CompanyName { get; set; } = string.Empty; 
    public decimal OpenPrice { get; set; }              
    public decimal HighPrice { get; set; }              
    public decimal LowPrice { get; set; }               
    public decimal ClosePrice { get; set; }             
    public decimal AveragePrice { get; set; }           
    public long TradedQuantity { get; set; }            
    public decimal TradedVolume { get; set; }  
}
