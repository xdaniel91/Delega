using Delega.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Infraestrutura.Database.Mapping;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {

        builder.ToTable("person");

        builder.HasKey(x => x.Id)
            .HasName("id");
       
        builder.Property(x => x.AddressId)
        .IsRequired()
        .HasColumnName("id_address");

        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<Person>(x => x.AddressId)
            .HasConstraintName("fk_address_person");

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(90)
            .HasColumnName("first_name");

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(90)
            .HasColumnName("last_name");

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnName("cpf");

        builder.Property(x => x.Age)
            .IsRequired()
            .HasColumnName("age");

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnName("birth_date");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
    }
}
