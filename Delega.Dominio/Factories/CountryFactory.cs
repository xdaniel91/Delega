using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Factories;

public static class CountryFactory
{
    private static readonly CountryValidator _validator = new CountryValidator();

    public static async Task<Country> CreateAsync(string name)
    {
        try
        {
            var country = new Country(name);

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
 