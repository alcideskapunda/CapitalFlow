using CapitalFlow.Domain.Entities.Rebalancing.EventIRs;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Rebalancing.EventIRs;

public sealed class EventIRConfiguration : IEntityTypeConfiguration<EventIR>
{
    public void Configure(EntityTypeBuilder<EventIR> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
            .WithMany(c => c.EventIRs)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.BaseAmount)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();

        builder.Property(x => x.IRAmount)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();
    }
}
