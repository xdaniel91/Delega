using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories;
public class LawyerRepository : ILawyerRepository
{
    private readonly DelegaContext Context;
    private readonly DbSet<Lawyer> DbSet;

    public LawyerRepository(DelegaContext context)
    {
        Context = context;
        DbSet = Context.lawyer;
    }

    public Lawyer Add(Lawyer lawyer)
    {
        try
        {
            var entry = DbSet.Add(lawyer);
            return entry.Entity;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public IEnumerable<Lawyer> GetAll()
    {
        return DbSet.AsEnumerable();
    }

    public async Task<Lawyer> GetByIdAsync(int id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public LawyerResponse GetResponse(int id)
    {
        var result = DbSet.Where(x => x.Id == id)
            .Include(x => x.Person)
            .FirstOrDefault();

        if (result is null)
            throw new Exception("Not found");

        return new LawyerResponse
        {
            Id = result.Id,
            Oab = result.Oab,
            PersonFirstName = result.Person.FirstName,
            PersonLastName = result.Person.LastName,
            PersonId = result.PersonId
        };

    }
}
