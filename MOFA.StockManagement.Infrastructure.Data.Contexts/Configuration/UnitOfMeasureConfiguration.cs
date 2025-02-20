using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOFA.StockManagement.Domain.Entities;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class UnitOfMeasureConfiguration : EntityTypeConfigurationBase<UnitOfMeasure, Guid>
    {
        public override void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.ToTable("UnitOfMeasures", "config");

            builder
                .Property(p => p.UnitName)
                .HasMaxLength(32)
                .IsRequired(true);

        }
    }
}
