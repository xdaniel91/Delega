using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delega.Api.Database.Mapping
{
    public class JudicialProcessMap : IEntityTypeConfiguration<JudicialProcess>
    {
        public void Configure(EntityTypeBuilder<JudicialProcess> builder)
        {

            builder.HasOne(x => x.Lawyer)
                    .WithOne()
                    .HasForeignKey<JudicialProcess>(x => x.LawyerId);

            builder.HasOne(x => x.Accused)
                    .WithOne()
                    .HasForeignKey<JudicialProcess>(x => x.AccusedId);

            builder.HasOne(x => x.Author)
                    .WithOne()
                    .HasForeignKey<JudicialProcess>(x => x.AuthorId);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.AccusedId).HasColumnName("accusedid");
            builder.Property(x => x.AuthorId).HasColumnName("authorid");
            builder.Property(x => x.LawyerId).HasColumnName("lawyerid");
            builder.Property(x => x.DateHourCreated).HasColumnName("datehourcreated");
            builder.Property(x => x.DateHourFinished).HasColumnName("datehourfinished");
            builder.Property(x => x.DateHourInProgress).HasColumnName("datehourinprogress");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.Value).HasColumnName("value");
            builder.Property(x => x.Verdict).HasColumnName("verdict");
            builder.Property(x => x.RequestedValue).HasColumnName("requestedvalue");
            builder.Property(x => x.Reason).HasColumnName("reason");
        }
    }
}
