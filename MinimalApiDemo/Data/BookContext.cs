using Microsoft.EntityFrameworkCore;

namespace MinimalApiDemo.Data
{
    public class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
    }
}
