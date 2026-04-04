using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Distributions;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.PurchasingAndDistribution.Distributions;

public sealed class DistributionConfiguration : IEntityTypeConfiguration<Distribution>
{
    public void Configure(EntityTypeBuilder<Distribution> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.BuyOrder)
            .WithMany(x => x.Distributions)
            .HasForeignKey(x => x.BuyOrderId)
            .IsRequired();
        builder.HasOne(x => x.Custodies)
            .WithMany(x => x.Distributions)
            .HasForeignKey(x => x.CustodyFilhoteId)
            .IsRequired();

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
