using Microsoft.EntityFrameworkCore;

namespace Loria.Core.Storages.SQLite
{
    public class Database : DbContext
    {
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=loria.db");
        }
    }
}
