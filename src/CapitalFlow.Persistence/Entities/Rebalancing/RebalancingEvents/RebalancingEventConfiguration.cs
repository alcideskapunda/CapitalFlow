using CapitalFlow.Domain.Entities.Rebalancing.RebalancingEvent;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Rebalancing.RebalancingEvents;

public sealed class RebalancingEventConfiguration : IEntityTypeConfiguration<RebalancingEvent>
{
    public void Configure(EntityTypeBuilder<RebalancingEvent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
            .WithMany(c => c.Rebalancings)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.RebalancingEventDate);
        builder.HasIndex(x => x.SoldTicker);
        builder.HasIndex(x => x.BoughtTicker);

        builder.Property(x => x.Type)
            .IsRequired();
        builder.Property(x => x.SoldTicker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();
        builder.Property(x => x.BoughtTicker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();
        builder.Property(x => x.SaleAmount)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();
        builder.Property(x => x.RebalancingEventDate)
            .IsRequired();



    }
}