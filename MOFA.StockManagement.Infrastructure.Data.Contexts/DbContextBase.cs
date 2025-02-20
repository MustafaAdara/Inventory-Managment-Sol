using Microsoft.EntityFrameworkCore;

namespace MOFA.StockManagement.Infrastructure.Data.Contexts
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options)
            : base(options)
        {
        }
    }
}
