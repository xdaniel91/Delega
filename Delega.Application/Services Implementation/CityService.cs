using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Factories;
using Delega.Dominio.Validators;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class CityService : ICityService
{
    private readonly CityValidator _cityValidator = new CityValidator();
    protected readonly ICityRepository _cityRepository;
    protected readonly IUow _uow;

    public CityService(ICityRepository cityRepository, IUow uow)
    {
        _cityRepository = cityRepository;
        _uow = uow;
    }


    public async Task<CityResponse> AddCityAsync(CityCreateDTO cityCad, CancellationToken cancellationToken)
    {
        try
        {
            var cityInsert = await CityFactory.CreateAsync(cityCad.Name, cityCad.StateId); 
            var insertedCity = await _cityRepository.AddCityAsync(cityInsert, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetCityAsync(insertedCity.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CityResponse> GetCityAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var city = await _cityRepository.GetCityAsync(id, cancellationToken);
           
            return new CityResponse
            {
                Name = city.Name,
                State = city.State.Name,
                StateId = city.StateId
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CityResponse> UpdateCityAsync(CityUpdateDTO cityUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var city = await _cityRepository.GetCityAsync(cityUpdate.Id, cancellationToken, true);
            var updatedCity = await _cityRepository.UpdateCityAsync(city, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetCityAsync(cityUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

}
