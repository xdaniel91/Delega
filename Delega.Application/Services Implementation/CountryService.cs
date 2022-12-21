using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Factories;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class CountryService : ICountryService
{
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
            var countryInsert = await CountryFactory.CreateAsync(countryCad.Name);
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
            await country.UpdateAsync(countryUpdate.Name, cancellationToken);
            var updatedCountry = await _countryRepository.UpdateCountryAsync(country, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetCountryAsync(countryUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }

}
