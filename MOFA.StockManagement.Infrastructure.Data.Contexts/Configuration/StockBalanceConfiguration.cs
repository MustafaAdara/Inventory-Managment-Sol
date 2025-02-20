using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class StockBalanceConfiguration : EntityTypeConfigurationBase<StockBalance, Guid>
    {
        public override void Configure(EntityTypeBuilder<StockBalance> builder)
        {
            builder.ToTable("StockBalances", "operation");

            builder
                .Property(p => p.MinimumStockLevel)
                .IsRequired(true);

            builder
                .Property(p => p.Quantity)
                .IsRequired(true);

            builder
                .Property(s => s.LastUpdatedAt)
                .IsRequired();

            builder
                .HasOne(p => p.Item)
                .WithMany(p => p.StockBalances)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder 
               .HasOne(s => s.Warehouse)
               .WithMany(p => p.StockBalances)
               .HasForeignKey(s => s.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
