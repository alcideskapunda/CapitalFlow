using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Customers.GraphicAccounts;

public sealed class GraphicAccountConfiguration : IEntityTypeConfiguration<GraphicAccount>
{
    public void Configure(EntityTypeBuilder<GraphicAccount> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
            .WithOne(x => x.GraphicAccount)
            .HasForeignKey<GraphicAccount>(x => x.CustumerId)
            .IsRequired();

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(ConfigurationConstants.AccountMaxLenth)
            .IsRequired();
    }
}
