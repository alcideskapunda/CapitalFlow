using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Customers.Customers;

public class CustomerContributionHistoryConfiguration : IEntityTypeConfiguration<CustomerContributionHistory>
{
    public void Configure(EntityTypeBuilder<CustomerContributionHistory> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Customer)
            .WithMany(c => c.ContributionHistories)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Amount)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.StartDate);
    }
}