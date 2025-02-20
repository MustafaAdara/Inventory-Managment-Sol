using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class WarehouseConfiguration : EntityTypeConfigurationBase<Warehouse, Guid>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses", "config");


            builder
                .Property(p => p.Code)
                .HasMaxLength(6)
                .IsRequired(true);

            builder
                .HasIndex(p => p.Code)
                .IsUnique();

            builder
                .Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .Property(p => p.Location)
                .HasMaxLength(64)
                .IsRequired(true);

            builder
                .Property(p => p.Capacity)
                .IsRequired(true);

            builder
                .Property(p => p.IsFull)
                .IsRequired(true);

        }
    }
}
