using Delega.Dominio.Entities;

namespace Delega.Infraestrutura.Repositories_Interfaces;

public interface ICountryRepository
{
    Task<Country> AddCountryAsync(Country country, CancellationToken cancellationToken);
    Task<Country> UpdateCountryAsync(Country country, CancellationToken cancellationToken);
    Task<Country> GetCountryAsync(long id, CancellationToken cancellationToken);
}
