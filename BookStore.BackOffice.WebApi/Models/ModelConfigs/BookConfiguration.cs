using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.BackOffice.WebApi.Models.ModelConfigs
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.Property(p => p.Title).HasMaxLength(100);
            builder.Property(p => p.ShortDescription).HasMaxLength(250);
            builder.Property(p => p.Price).HasColumnType("Money");
        }
    }
}
