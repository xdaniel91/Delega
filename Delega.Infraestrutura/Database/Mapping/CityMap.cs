using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class CityMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("city");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.StateId).HasColumnName("id_state").IsRequired();
        builder.HasOne(x => x.State).WithOne().HasForeignKey<City>(x => x.StateId).HasConstraintName("fk_state_city");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(59);
    }
}
