using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class TransactionDetailConfiguration : EntityTypeConfigurationBase<TransactionDetail, Guid>
    {
        public override void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            builder.ToTable("TransactionDetails", "operation");

            builder
                .Property(p => p.UnitCost)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(p => p.Quantity)
                .IsRequired(true);

            builder
                .HasOne(p => p.Transaction)
                .WithMany(p => p.TransactionDetails)
                .HasForeignKey(p => p.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Item)
                .WithMany()
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(p => p.OrderDetail)
               .WithMany()
               .HasForeignKey(p => p.OrderDetailId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
