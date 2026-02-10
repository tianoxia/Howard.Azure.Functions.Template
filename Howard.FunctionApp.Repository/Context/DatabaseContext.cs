using Microsoft.EntityFrameworkCore;
using Howard.FunctionApp.Repository.Tables;

namespace Howard.FunctionApp.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        #region Tables
        public DbSet<Item> Items { get; set; }
        #endregion
    }
}
