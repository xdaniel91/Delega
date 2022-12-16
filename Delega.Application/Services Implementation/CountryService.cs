using Delega.Application.Exceptions;
using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Entities;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class CountryService : ICountryService
{
    private readonly CountryValidator _countryValidator = new CountryValidator();
    protected readonly ICountryRepository _countryRepository;
    protected readonly IUow _uow;

    public CountryService(ICountryRepository repository, IUow uow)
    {
        _countryRepository = repository;
        _uow = uow;
    }

    public async Task<CountryResponse> AddCountryAsync(CountryCreateDTO countryCad, CancellationToken cancellationToken)
    {
        try
        {
            var countryInsert = new Country(countryCad.Name);
            await ValidarAsync(countryInsert, cancellationToken);
            var insertedCountry = await _countryRepository.AddCountryAsync(countryInsert, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return new CountryResponse
            {
                Name = insertedCountry.Name
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CountryResponse> GetCountryAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var country = await _countryRepository.GetCountryAsync(id, cancellationToken);
            return new CountryResponse
            {
                Name = country.Name
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<CountryResponse> UpdateCountryAsync(CountryUpdateDTO countryUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var country = await _countryRepository.GetCountryAsync(countryUpdate.Id, cancellationToken, true);

            if (countryUpdate.Name != null)
                country.Name = countryUpdate.Name;

            await ValidarAsync(country, cancellationToken);

            var updatedCountry = await _countryRepository.UpdateCountryAsync(country, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetCountryAsync(countryUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task ValidarAsync(Country country, CancellationToken cancellationToken)
    {
        var valitionResult = await _countryValidator.ValidateAsync(country, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(", ", errors);
            throw new DelegaApplicationException(errorsString);
        }
    }
}
