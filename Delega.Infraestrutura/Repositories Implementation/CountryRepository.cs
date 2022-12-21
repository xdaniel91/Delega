using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Repositories_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delega.Infraestrutura.Repositories_Implementation;

public class CountryRepository : ICountryRepository
{
    protected readonly DelegaContext Context;
    protected readonly DbSet<Country> Countries;

    public CountryRepository(DelegaContext dbContext)
    {
        Context = dbContext;
        Countries = Context.country;
    }

    public async Task<Country> AddCountryAsync(Country country, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Countries.AddAsync(country, cancellationToken);
            return result.Entity;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Country> GetCountryAsync(long id, CancellationToken cancellationToken, bool trackObj)
    {
        try
        {
            var country = trackObj ? await

                 (from _state in Countries
                         .Where(x => x.Id == id)
                  select _state).SingleOrDefaultAsync(cancellationToken)
                             :
              await (from _state in Countries
                         .AsNoTracking()
                         .Where(x => x.Id == id)
                     select _state).SingleOrDefaultAsync(cancellationToken);

            return country is null ? throw new DelegaDataException("Country not found") : country;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Country> UpdateCountryAsync(Country country, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new DelegaDataException("Request cancelled");

        try
        {
            var result = Countries.Update(country);

            return result.Entity;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
