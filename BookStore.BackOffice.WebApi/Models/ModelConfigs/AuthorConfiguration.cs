using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.BackOffice.WebApi.Models.ModelConfigs
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Firstname).HasMaxLength(80);
            builder.Property(p => p.Lastname).HasMaxLength(80);
        }
    }
}