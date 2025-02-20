using Microsoft.EntityFrameworkCore;
using MOFA.StockManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration
{
    public class EntityTypeConfigurationBase<TEntity, T> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase<T> where T : notnull
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .HasKey(pk => pk.Id);


            if (typeof(T) == typeof(Guid))
            {
                builder
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();
            }

            builder
                .Property(p => p.ModifiedBy)
                .HasMaxLength(128);

            builder
                .Property(p => p.ModifiedAt)
                .IsRequired(true);

            builder
                .Property(p => p.RowVersion)
                .IsRowVersion();
        }
    }
}
