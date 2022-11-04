using Delega.Api.Database.Mapping;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Database;

public class DelegaContext : DbContext
{


    public DbSet<Person> person { get; set; }
    public DbSet<Lawyer> lawyer { get; set; }
    public DbSet<Author> author { get; set; }
    public DbSet<Accused> accused { get; set; }
    public DbSet<JudicialProcess> judicialprocess { get; set; }
    public DelegaContext(DbContextOptions<DelegaContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PersonMap());
        builder.ApplyConfiguration(new LawyerMap());
        builder.ApplyConfiguration(new JudicialProcessMap());
        builder.ApplyConfiguration(new AccusedMap());
        builder.ApplyConfiguration(new AuthorMap());
    }
}
