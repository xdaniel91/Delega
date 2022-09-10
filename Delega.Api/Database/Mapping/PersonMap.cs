using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Api.Database.Mapping
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.BirthDate).HasColumnName("birthdate").HasDefaultValueSql("now()");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.FirstName).HasColumnName("firstname");
            builder.Property(x => x.LastName).HasColumnName("lastname");
            builder.Property(x => x.Cpf).HasColumnName("cpf");
            builder.Property(x => x.CreatedTime).HasColumnName("createdtime");
            builder.Property(x => x.UpadatedTime).HasColumnName("updatedtime");
        }
    }
}
