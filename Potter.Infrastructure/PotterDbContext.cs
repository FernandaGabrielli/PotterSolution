using Microsoft.EntityFrameworkCore;
using Potter.Domain.Entities;

namespace Potter.Infrastructure
{
    public class PotterDbContext : DbContext
    {
        public PotterDbContext(DbContextOptions<PotterDbContext> options) : base(options) {}

        // Construtor sem parâmetros para suportar migrações
        public PotterDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PotterDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
