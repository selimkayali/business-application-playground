using BookStore.BackOffice.WebApi.Models.ModelConfigs;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BackOffice.WebApi.Models
{
    public class BookStoreDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> dbContextOptions) : base(dbContextOptions){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new AuthorConfiguration());
        }
    }
}