using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class StateMap : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("state");
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CountryId).IsRequired().HasColumnName("id_country");
        builder.HasOne(x => x.Country).WithOne().HasForeignKey<State>(x => x.CountryId).HasConstraintName("fk_country_state");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(60);
    }
}
