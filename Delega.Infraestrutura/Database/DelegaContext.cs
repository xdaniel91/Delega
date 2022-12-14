using Delega.Infraestrutura.Database.Mapping;
using Delega.Dominio.Entities;
using Delega.Infraestrutura.Database.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Delega.Infraestrutura.Database;

public class DelegaContext : DbContext
{

    public DbSet<Person> person { get; set; }
    public DbSet<Address> address { get; set; }
    public DbSet<City> city { get; set; }
    public DbSet<State> state { get; set; }
    public DbSet<Country> country { get; set; }

    public DelegaContext(DbContextOptions<DelegaContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PersonMap());
    }
}
