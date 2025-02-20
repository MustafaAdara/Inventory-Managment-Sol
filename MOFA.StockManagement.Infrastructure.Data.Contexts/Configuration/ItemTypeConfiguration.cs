using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class ItemTypeConfiguration : EntityTypeConfigurationBase<ItemType, Guid>
    {
        public override void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.ToTable("ItemTypes", "config");

            builder
                .Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .Property(p => p.Description)
                .HasMaxLength(64)
                .IsRequired(true);

            builder
                .Property(p => p.ParentItemTypeID)
                .IsRequired(false);
        }
    }
}