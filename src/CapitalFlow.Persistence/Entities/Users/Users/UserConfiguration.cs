using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.Users.Users;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //Primary Key
        builder.HasKey(x => x.Id);

        //Relationships
        builder.HasOne(x => x.Customer)
            .WithOne(c => c.User)
            .HasForeignKey<Customer>(c => c.UserId);

        //Properties
        builder.Property(x => x.Name)
            .HasMaxLength(ConfigurationConstants.NameMaxLength)
            .IsRequired();
        builder.Property(x => x.Email)
            .HasMaxLength(ConfigurationConstants.EmailMaxLength)
            .IsRequired();
        builder.Property(x => x.Password)
            .HasMaxLength(ConfigurationConstants.PasswordMaxLength)
            .IsRequired();
        builder.Property(x => x.AccessLevel)
            .IsRequired();
        builder.Property(x => x.EmailVerified)
            .IsRequired();
        builder.Property(x => x.ResetPassword)
            .IsRequired();
    }
}
