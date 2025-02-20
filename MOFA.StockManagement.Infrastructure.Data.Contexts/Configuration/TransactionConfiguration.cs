using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class TransactionConfiguration : EntityTypeConfigurationBase<Transaction, Guid>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions", "operation");

            builder
                .Property(p => p.TransactionDate)
                .IsRequired(true);

            builder
                .Property(p => p.TransactionType)
                .IsRequired(true);

            builder
                .HasOne(p => p.Supplier)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(t => t.SourceWarehouse)
                   .WithMany(t => t.SourceTransactions) 
                   .HasForeignKey(t => t.SourceWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DestinationWarehouse)
                   .WithMany(t => t.DestinationTransactions) 
                   .HasForeignKey(t => t.DestinationWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Order)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
