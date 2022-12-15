using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CityId).IsRequired().HasColumnName("id_city");
        builder.HasOne(x => x.City).WithOne().HasForeignKey<Address>(x => x.CityId).HasConstraintName("fk_city_address");
        builder.Property(x => x.Street).IsRequired().HasColumnName("street");
        builder.Property(x => x.ZipCode).IsRequired().HasColumnName("zip_code").HasMaxLength(20);
        builder.Property(x => x.Number).HasColumnName("number");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information").HasMaxLength(60);
        builder.Property(x => x.District).IsRequired().HasColumnName("district");
    }
}
