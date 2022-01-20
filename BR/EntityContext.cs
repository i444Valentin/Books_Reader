using Microsoft.EntityFrameworkCore;

namespace BooksReader.App
{
    class EntityContext : DbContext
    {

        public EntityContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=12345678;database=booksreader;");
        }
    }
}