using CapitalFlow.Domain.Entities.Quotes;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Quotes;

public class B3StockQuoteConfiguration : IEntityTypeConfiguration<B3StockQuote>
{
    public void Configure(EntityTypeBuilder<B3StockQuote> builder)
    {
        builder.HasKey(x => new { x.Ticker, x.TradingDate });

        builder.Property(x => x.Ticker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();

        builder.Property(x => x.TradingDate)
            .IsRequired();

        builder.Property(x => x.CompanyName)
            .HasMaxLength(150);

        builder.Property(x => x.BdiCode)
            .HasMaxLength(10);

        // Preços (OHLC + Average)
        builder.Property(x => x.OpenPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();

        builder.Property(x => x.HighPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();

        builder.Property(x => x.LowPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();

        builder.Property(x => x.ClosePrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();

        builder.Property(x => x.AveragePrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2);

        // Dados de Negociação
        builder.Property(x => x.TradedQuantity)
            .IsRequired();

        builder.Property(x => x.TradedVolume)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2);

        // Índice para buscas rápidas apenas por ticker (ordenado pela data mais recente)
        builder.HasIndex(x => new { x.Ticker, x.TradingDate })
            .IsDescending(false, true);
    }
}