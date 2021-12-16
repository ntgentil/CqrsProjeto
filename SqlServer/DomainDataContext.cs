using Core.Adapters.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SqlServer
{
    public class DomainDataContext : DbContext
    {
        private readonly ISqlServerStoreHolder _storeHolder;

        public DomainDataContext(ISqlServerStoreHolder storeHolder)
        {
            _storeHolder = storeHolder;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_storeHolder.GetDbConnection("CQRS").ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
