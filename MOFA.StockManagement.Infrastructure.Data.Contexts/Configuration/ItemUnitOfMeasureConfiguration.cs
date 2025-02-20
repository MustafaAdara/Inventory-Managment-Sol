using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class ItemUnitOfMeasureConfiguration : EntityTypeConfigurationBase<ItemUnitOfMeasure, Guid>
    {
        public override void Configure(EntityTypeBuilder<ItemUnitOfMeasure> builder)
        {
            builder.ToTable("ItemUnitOfMeasures", "config");

            builder
                .Property(p => p.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(p => p.IsBase)
                .IsRequired(true);

            builder
                .Property(p => p.ConversionFactor)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(p => p.Item)
                .WithMany(p => p.ItemUnitOfMeasures)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.UnitOfMeasure)
                .WithMany(p => p.ItemUnitOfMeasures)
                .HasForeignKey(p => p.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
