using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class OrderDetailConfiguration : EntityTypeConfigurationBase<OrderDetail, Guid>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails", "operation");

            builder
                .Property(p => p.Quantity)
                .IsRequired(true);

            builder
                .Property(p => p.UnitPrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(p => p.OrderItemStatus)
                .IsRequired(true);

            builder
                .HasOne(p => p.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Item)
                .WithMany()
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
