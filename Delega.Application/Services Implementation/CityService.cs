using Delega.Application.Exceptions;
using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Entities;
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

    public async Task<CityResponse> AddCityAsync(CityCreateDTO cityCad, CancellationToken cancellationToken)
    {
        try
        {
            var cityInsert = new City(cityCad.Name, cityCad.StateId);
          
            await ValidarAsync(cityInsert, cancellationToken);
           
            var insertedCity = await _cityRepository.AddCityAsync(cityInsert, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);
          
            return new CityResponse
            {
                Name = insertedCity.Name,
                State = insertedCity.State.Name,
                StateId = insertedCity.StateId
            };
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
            var city = await _cityRepository.GetCityAsync(cityUpdate.Id, cancellationToken);

            if (cityUpdate.Name != null)
                city.Name = cityUpdate.Name;

            await ValidarAsync(city, cancellationToken);
            
            var updatedCity = await _cityRepository.UpdateCityAsync(city, cancellationToken);

            var result = await _uow.CommitAsync(cancellationToken);
            
            return new CityResponse
            {
                Name = updatedCity.Name,
                State = updatedCity.State.Name,
                StateId = updatedCity.StateId
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ValidarAsync(City city, CancellationToken cancellationToken)
    {
        var valitionResult = await _cityValidator.ValidateAsync(city, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(", ", errors);
            throw new DelegaApplicationException(errorsString);
        }
    }
}
