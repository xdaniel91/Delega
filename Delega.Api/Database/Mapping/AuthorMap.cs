using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Api.Database.Mapping
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("now()");
            builder.Property(x => x.UpadatedTime).HasDefaultValueSql("now()");

            builder.HasOne(x => x.Person)
                .WithOne()
                .HasForeignKey<Author>(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.PersonId).HasColumnName("personid");
            builder.Property(x => x.UpadatedTime).HasColumnName("updatedtime");
            builder.Property(x => x.Depoiment).HasColumnName("depoiment");
            builder.Property(x => x.CreatedTime).HasColumnName("createdtime");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Cpf).HasColumnName("cpf");
        }
    }
}
