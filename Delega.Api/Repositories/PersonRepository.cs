using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories.Implementation;

public class PersonRepository : IPersonRepository
{
    private readonly DelegaContext Context;
    
    public PersonRepository(DelegaContext context)
    {
        Context = context;
    }

    public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken)
    {
        try
        {
            var entry = await Context.person.AddAsync(person, cancellationToken);
            return entry.Entity;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return Context.person.AsEnumerable();
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        return await Context.person.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}
