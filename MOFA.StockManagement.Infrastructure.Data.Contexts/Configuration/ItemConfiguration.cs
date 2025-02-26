using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class ItemConfiguration : EntityTypeConfigurationBase<Item, Guid>
    {
        public override void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", "sales");

            builder
                .Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .Property(p => p.Description)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .Property(p => p.BarCode)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .HasIndex(p => p.BarCode)
                .IsUnique();

            builder
                .Property(p => p.SKU)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .HasIndex(p => p.SKU)
                .IsUnique();

            builder
                .Property(p => p.UnitPrice)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(p => p.CreatedAt)
                .IsRequired(true);

            builder
                .Property(p => p.ItemTypeId)
                .IsRequired(true);

            builder
                .HasOne(p => p.ItemType)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.ItemTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
