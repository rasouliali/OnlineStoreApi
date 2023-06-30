using OnlineStoreApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStoreApi.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.ImagePath)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.LastModifiedBy)
            .HasMaxLength(200)
            .IsRequired();
        builder.HasOne(t => t.CurrentColor)
            .WithMany()
            .HasForeignKey(t=>t.ColorId);
        builder.HasOne(t => t.CurrentSize)
            .WithMany()
            .HasForeignKey(t=>t.SizeId);
    }
}
