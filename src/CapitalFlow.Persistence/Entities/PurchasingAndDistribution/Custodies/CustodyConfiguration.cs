using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.PurchasingAndDistribution.Custodies;

public sealed class CustodyConfiguration : IEntityTypeConfiguration<Custody>
{
    public void Configure(EntityTypeBuilder<Custody> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.GraphicAccount)
            .WithMany(x => x.Custodies)
            .HasForeignKey(x => x.GraphicAccountId)
            .IsRequired();
        builder.HasMany(x => x.Distributions)
            .WithOne(x => x.Custodies)
            .HasForeignKey(x => x.CustodyFilhoteId)
            .IsRequired();

        builder.Property(x => x.Ticker)
            .HasMaxLength(ConfigurationConstants.TickerMaxLength)
            .IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.Property(x => x.AveragePrice)
            .HasColumnType(ConfigurationConstants.MoneyPrecision4)
            .IsRequired();

    }
}
