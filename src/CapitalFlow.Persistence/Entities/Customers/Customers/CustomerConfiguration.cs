using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Customers.Customers;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(u => u.User)
            .WithOne(c => c.Customer)
            .HasForeignKey<Customer>(e => e.UserId)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.CPF).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(e => e.Name)
            .HasMaxLength(ConfigurationConstants.NameMaxLength)
            .IsRequired();
        builder.Property(e => e.CPF)
            .HasMaxLength(ConfigurationConstants.CpfMaxLength)
            .IsRequired();
        builder.Property(e => e.Email)
            .HasMaxLength(ConfigurationConstants.EmailMaxLength)
            .IsRequired();
        builder.Property(e => e.MonthlyAmount)
            .HasColumnType(ConfigurationConstants.MoneyPrecision2)
            .IsRequired();
    }
}
