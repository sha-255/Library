using AdvertisingAgency.Common;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Context
{
    class ReadersContext : DbContext
    {
        public DbSet<Reader> Readers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(Settings.SqlConnection);
    }
}
