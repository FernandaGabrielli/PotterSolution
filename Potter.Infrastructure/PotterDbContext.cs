using Microsoft.EntityFrameworkCore;

namespace Potter.Infrastructure
{
    public class PotterDbContext : DbContext
    {
        public PotterDbContext(DbContextOptions<PotterDbContext> options) : base(options) {}

        public DbSet<Character> Characters { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
