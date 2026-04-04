using CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;
using CapitalFlow.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalFlow.Persistence.Entities.RecommendationBasket.RecommendationBasket;

public sealed class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.BasketItems)
            .WithOne(x => x.Basket)
            .HasForeignKey(x => x.BasketId)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(ConfigurationConstants.NameMinLength)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
    }
}