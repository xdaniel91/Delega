using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Repositories_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delega.Infraestrutura.Repositories_Implementation;

public class CityRepository : ICityRepository
{
    protected readonly DelegaContext Context;
    protected readonly DbSet<City> Cities;

    public CityRepository(DelegaContext dbContext)
    {
        Context = dbContext;
        Cities = Context.city;
    }


    public async Task<City> AddCityAsync(City city, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Cities.AddAsync(city, cancellationToken);
            return result.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<City> GetCityAsync(long id, CancellationToken cancellationToken, bool trackObj = false)
    {
        try
        {
            var city = trackObj ? await

                 (from _city in Cities
                         .Where(x => x.Id == id)
                         .Include(x => x.State)
                  select _city).SingleOrDefaultAsync(cancellationToken)
                             :
              await (from _state in Cities
                         .AsNoTracking()
                         .Where(x => x.Id == id)
                         .Include(x => x.State)
                     select _state).SingleOrDefaultAsync(cancellationToken);

            return city is null ? throw new DelegaDataException("City not found") : city;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<City> UpdateCityAsync(City city, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new DelegaDataException("Request cancelled");

        try
        {
            var result = Cities.Update(city);
            return result.Entity;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
