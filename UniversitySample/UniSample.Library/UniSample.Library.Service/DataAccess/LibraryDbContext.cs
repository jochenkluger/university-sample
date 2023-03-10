using Microsoft.EntityFrameworkCore;
using UniSample.Library.Service.Model;

namespace UniSample.Library.Service.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Lending> Lendings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = Guid.Parse("6EEE2D39-041A-4FFD-9A27-D175C2A35794"),
                    Author = "Maarten van Steen, Andrew S. Tanenbaum",
                    Available = true,
                    ISBN = "978-1543057386",
                    Title = "Distributed Systems"
                },
                new Book()
                {
                    Id = Guid.Parse("CB37C704-9AC3-4436-B609-648B711D5938"),
                    Author = "Günther Bengel",
                    Available = true,
                    ISBN = "978-3834816702",
                    Title = "Grundkurs Verteilte Systeme"
                });
        }
    }
}
