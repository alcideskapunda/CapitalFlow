using CapitalFlow.Domain.Entities.PurchasingAndDistribution.BuyOrders;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.PurchasingAndDistribution.BuyOrders;

public sealed class BuyOrderConfiguration : IEntityTypeConfiguration<BuyOrder>
{
    public void Configure(EntityTypeBuilder<BuyOrder> builder)
    {
        builder.HasOne(x => x.GraphicAccount)
            .WithMany(x => x.BuyOrders)
            .HasForeignKey(x => x.ContaMasterId)
            .IsRequired();

        builder.HasIndex(x => x.Ticker);
        builder.HasIndex(x => x.ExecutedAt);

        builder.Property(x => x.Ticker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.Property(x => x.UnitPrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();
    }
}
