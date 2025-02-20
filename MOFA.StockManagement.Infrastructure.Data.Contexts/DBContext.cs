using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using MOFA.StockManagement.Infrastructure.Data.Contexts.Configuration;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts
{
    public class DBContext : DbContextBase
    {
        // SET api as startup project, package manager console to MOFA.StockManagement.Infrastructure.Data.Contexts
        ///// add-migration Init -context MOFA.StockManagement.Infrastructure.Data.Contexts.DBContext
        ///// Remove-Migration -context MOFA.StockManagement.Infrastructure.Data.Contexts.DBContext
        ///// update-database -context MOFA.StockManagement.Infrastructure.Data.Contexts.DBContext
        ///// update-database 0 -context MOFA.StockManagement.Infrastructure.Data.Contexts.DBContext
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConsumerConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemUnitOfMeasureConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderSerialConfiguration());
            modelBuilder.ApplyConfiguration(new StockBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierItemConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());


            base.OnModelCreating(modelBuilder);


        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            ArgumentNullException.ThrowIfNull(configurationBuilder);

            configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeAsUtcValueConverter>();
            configurationBuilder.Properties<DateTime?>().HaveConversion<NullableDateTimeAsUtcValueConverter>();
        }
        //just a future idea
        public class NullableDateTimeAsUtcValueConverter : ValueConverter<DateTime?, DateTime?>
        {
            public NullableDateTimeAsUtcValueConverter() : base(v => !v.HasValue ? v : ToUtc(v.Value), v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v)
            {

            }
            private static DateTime? ToUtc(DateTime v) => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime();
        }

        public class DateTimeAsUtcValueConverter : ValueConverter<DateTime, DateTime>
        {
            public DateTimeAsUtcValueConverter() : base(v => DateTime.SpecifyKind(ToUtc(v), DateTimeKind.Utc), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            {

            }
            private static DateTime ToUtc(DateTime v) => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime();
        }
    }
}
