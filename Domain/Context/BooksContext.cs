using AdvertisingAgency.Common;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Context
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(Settings.SqlConnection);
    }
}
