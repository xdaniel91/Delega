using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;

namespace Delega.Infraestrutura.Services_Interfaces;

public interface ICountryService
{
    Task<CountryResponse> AddCountryAsync(CountryCreateDTO countryCad, CancellationToken cancellationToken);
    Task<CountryResponse> UpdateCountryAsync(CountryUpdateDTO countryUpdate, CancellationToken cancellationToken);
    Task<CountryResponse> GetCountryAsync(long id, CancellationToken cancellationToken);
}
