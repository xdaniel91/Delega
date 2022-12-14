using Delega.Dominio.Entities;

namespace Delega.Infraestrutura.Repositories_Interfaces;

public interface ICityRepository
{
    Task<City> AddCityAsync(City city, CancellationToken cancellationToken);
    Task<City> UpdateCityAsync(City city, CancellationToken cancellationToken);
    Task<City> GetCityAsync(long id, CancellationToken cancellationToken);
}
