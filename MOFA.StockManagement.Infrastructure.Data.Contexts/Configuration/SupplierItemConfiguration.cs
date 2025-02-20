using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class SupplierItemConfiguration : EntityTypeConfigurationBase<SupplierItem, Guid>
    {
        public override void Configure(EntityTypeBuilder<SupplierItem> builder)
        {
            builder.ToTable("SupplierItems", "sales");

            builder
                .Property(p => p.PurchasePrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(p => p.DateSupplied)
                .IsRequired(true);

            builder
                .Property(p => p.Quantity)
                .IsRequired(true);

            builder
                .Property(p => p.Unit)
                .IsRequired(true);

            builder
                .HasOne(p => p.Item)
                .WithMany(p => p.SupplierItems)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Supplier)
                .WithMany(p => p.SupplierItems)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
