using CapitalFlow.Domain.Entities.Rebalancing.RebalancingEvent;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Rebalancing.RebalancingEvent;

public sealed class RebalancingEventConfiguration : IEntityTypeConfiguration<RebalancingEvent>
{
    public void Configure(EntityTypeBuilder<RebalancingEvent> builder)
    {
        // 🔑 PK
        builder.HasKey(x => x.Id);

        // 🔗 RELACIONAMENTO (Customer → RebalancingEvents)
        builder.HasOne(x => x.Customer)
            .WithMany(c => c.RebalancingEvents)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        // 📌 PROPRIEDADES
        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.SoldTicker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();

        builder.Property(x => x.BoughtTicker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();

        builder.Property(x => x.SaleAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.RebalancingEventDate)
            .IsRequired();

        // 🔍 INDEXES (performance)
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.RebalancingEventDate);
        builder.HasIndex(x => x.SoldTicker);
        builder.HasIndex(x => x.BoughtTicker);
    }
}