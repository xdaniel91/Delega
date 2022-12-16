using Delega.Application.Exceptions;
using Delega.Dominio.Entities;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Repositories_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delega.Infraestrutura.Repositories_Implementation;

public class PersonRepository : IPersonRepository
{
    private readonly DelegaContext Context;
    private readonly DbSet<Person> People;
    
    public PersonRepository(DelegaContext context)
    {
        Context = context;
        People = Context.person;
    }

    public async Task<Person> AddPersonAsync(Person person, CancellationToken cancellationToken)
    {
        try
        {
            var result = await People.AddAsync(person, cancellationToken);
            return result.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Person> GetPersonAsync(long id, CancellationToken cancellationToken, bool trackObj = false)
    {
        try
        {
            var person = trackObj ? await

                (from _person in People
                        .Where(x => x.Id == id)
                        .Include(x => x.Address)
                 select _person).SingleOrDefaultAsync(cancellationToken)
                            :
             await (from _person in People
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .Include(x => x.Address)
                    select _person).SingleOrDefaultAsync(cancellationToken);

            return person is null ? throw new DelegaDataException("Person not found") : person;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Person> UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new DelegaDataException("Request cancelled");

        try
        {
            var result = People.Update(person);
            return result.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
