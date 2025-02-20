using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.ToTable("Orders", "operation");


            builder.Property(p => p.Type)
                   .IsRequired(true);

            builder.Property(p => p.Status)
                   .IsRequired(true);

            builder.Property(p => p.Number)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.HasIndex(o => o.Number)
                   .IsUnique();

            builder.Property(p => p.OrderDate)
                   .IsRequired();


            builder.HasOne(o => o.Supplier)
                   .WithMany(s => s.Orders)
                   .HasForeignKey(o => o.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Warehouse)
                   .WithMany(o => o.Orders)
                   .HasForeignKey(o => o.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.DestinationWarehouse)
                   .WithMany()
                   .HasForeignKey(o => o.DestinationWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Consumer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.ConsumerId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(o => o.Transaction)
                   .WithOne(t => t.Order)
                   .HasForeignKey<Transaction>(t => t.OrderId)
                   .OnDelete(DeleteBehavior.NoAction); // If order is deleted.

        }
    }
}
