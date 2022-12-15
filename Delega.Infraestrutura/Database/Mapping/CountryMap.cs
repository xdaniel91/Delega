using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class CountryMap : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("country");
        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.Name).IsRequired().HasColumnName("name").HasMaxLength(60);
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
    }
}
