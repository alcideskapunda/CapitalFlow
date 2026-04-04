using CapitalFlow.Domain.Entities.Quotes;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Quotes;

public sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Ticker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();
        builder.Property(x => x.OpenPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();
        builder.Property(x => x.ClosePrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();
        builder.Property(x => x.HighPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();
        builder.Property(x => x.LowPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();
    }
}

