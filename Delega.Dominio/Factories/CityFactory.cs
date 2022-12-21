using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Factories;

public static class CityFactory
{
    private static readonly CityValidator _validator = new CityValidator();

    public static async Task<City> CreateAsync(string name, long stateId)
    {
        try
        {
            var country = new City(name, stateId);

            var valitionResult = await _validator.ValidateAsync(country);

            if (!valitionResult.IsValid)
            {
                var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
                var errorsString = string.Join(", ", errors);
                throw new DelegaDomainException(errorsString);
            }

            return country;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
