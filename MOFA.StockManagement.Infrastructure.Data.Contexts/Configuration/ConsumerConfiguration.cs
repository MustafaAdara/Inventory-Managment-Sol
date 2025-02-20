using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class ConsumerConfiguration : EntityTypeConfigurationBase<Consumer, Guid>
    {
        public override void Configure(EntityTypeBuilder<Consumer> builder)
        {
            builder.ToTable("Consumers", "sales");

            builder
                .Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired(true);

            builder
               .Property(p => p.Phone)
               .HasMaxLength(32)
               .IsRequired(true);

            builder
               .Property(p => p.Email)
               .HasMaxLength(32)
               .IsRequired(true);

            builder
               .Property(p => p.Address)
               .HasMaxLength(32)
               .IsRequired(true);

            builder
                .Property(p => p.Type)
                .IsRequired(true);

        }
    }
}
