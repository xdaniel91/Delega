using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class CityMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("city");

        builder.HasKey(x => x.Id)
            .HasName("id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(59);

        builder.Property(x => x.StateId)
            .IsRequired()
            .HasColumnName("id_state");

        builder.HasOne(x => x.State)
            .WithOne()
            .HasForeignKey<City>(o => o.StateId)
            .HasConstraintName("fk_state_city");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
    }
}
