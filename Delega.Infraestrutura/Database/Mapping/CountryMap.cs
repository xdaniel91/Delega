using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class CountryMap : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("country");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasColumnName("name").HasMaxLength(60);
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
    }
}
