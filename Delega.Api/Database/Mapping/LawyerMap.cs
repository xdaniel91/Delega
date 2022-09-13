using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Api.Database.Mapping
{
    public class LawyerMap : IEntityTypeConfiguration<Lawyer>
    {
        public void Configure(EntityTypeBuilder<Lawyer> builder)
        {
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("now()");
            builder.Property(x => x.UpadatedTime).HasDefaultValueSql("now()");

            builder.HasOne(x => x.Person)
                .WithOne()
                .HasForeignKey<Lawyer>(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.PersonId).HasColumnName("personid");
            builder.Property(x => x.UpadatedTime).HasColumnName("updatedtime");
            builder.Property(x => x.Oab).HasColumnName("oab");
            builder.Property(x => x.CreatedTime).HasColumnName("createdtime");
        }
    }
}
