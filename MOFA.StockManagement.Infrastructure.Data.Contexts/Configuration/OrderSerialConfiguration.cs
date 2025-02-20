using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class OrderSerialConfiguration : EntityTypeConfigurationBase<OrderSerial, Guid>
    {
        public override void Configure(EntityTypeBuilder<OrderSerial> builder)
        {
            builder.ToTable("OrderSerials", "config");

            builder
                .Property(p => p.Type)
                .IsRequired(true);

            builder
                .Property(p => p.Year)
                .IsRequired(true);

            builder
                .HasOne(p => p.Warehouse)
                .WithMany(p => p.OrderSerials)
                .HasForeignKey(p => p.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
