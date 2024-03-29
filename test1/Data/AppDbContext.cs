using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Data
{
 
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Author> Authors { get; set; }
            public DbSet<book> Books { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {

                // Define relationship between books and authors
                builder.Entity<book>()
                    .HasOne(x => x.Author)
                    .WithMany(x => x.Books);

                // Seed database with authors and books for demo
                new DbInitializer(builder).Seed();
            }
        }
    }