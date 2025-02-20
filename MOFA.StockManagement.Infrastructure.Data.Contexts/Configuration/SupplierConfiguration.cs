using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class SupplierConfiguration : EntityTypeConfigurationBase<Supplier, Guid>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers", "sales");

            builder
                .Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .Property(p => p.Phone)
                .IsRequired(true);

            builder
                .Property(p => p.Email)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
                .Property(p => p.Address)
                .HasMaxLength(64)
                .IsRequired(true);


        }
    }
}
