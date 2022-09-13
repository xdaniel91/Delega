using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories.Implementation;

public class PersonRepository : IPersonRepository
{
    private readonly DelegaContext Context;
    private readonly DbSet<Person> DbSet;

    public PersonRepository(DelegaContext context)
    {
        Context = context;
        DbSet = this.Context.person;
    }

    public Person Add(Person person)
    {
        var entry = DbSet.Add(person);
        return entry.Entity;
    }

    public void Delete(Person person)
    {
        DbSet.Remove(person);
    }

    public IEnumerable<Person> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    public Person GetById(int id)
    {
        return DbSet.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
    }

    public Person Update(Person person)
    {
        var entry = DbSet.Update(person);
        return entry.Entity;
    }
}
