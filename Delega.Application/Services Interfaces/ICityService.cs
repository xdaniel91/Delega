using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;

namespace Delega.Infraestrutura.Services_Interfaces;

public interface ICityService
{
    Task<CityResponse> AddCityAsync(CityCreateDTO CityCad, CancellationToken cancellationToken);
    Task<CityResponse> UpdateCityAsync(CityUpdateDTO CityUpdate, CancellationToken cancellationToken);
    Task<CityResponse> GetCityAsync(long id, CancellationToken cancellationToken);
}
