using Delega.Application.Exceptions;
using Delega.Dominio.Entities;
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

    public async Task<Country> GetCountryAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var country = await Countries.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

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
