using CapitalFlow.Domain.Entities.RecommendationBasket.BasketItems;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.RecommendationBasket.BasketItems;

public sealed class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Basket)
            .WithMany(x => x.BasketItems)
            .HasForeignKey(x => x.BasketId)
            .IsRequired();

        builder.HasIndex(x => new { x.BasketId, x.Ticker })
            .IsUnique();
        builder.HasIndex(x => x.Ticker);

        builder.Property(x => x.Ticker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();
        builder.Property(x => x.Percentage)
            .HasColumnType(ConfigurationConstants.PercentagePrecision)
            .IsRequired();
    }
}