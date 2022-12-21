using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Factories;

public static class AddressFactory
{
    private static readonly AddressValidator _validator = new AddressValidator();

    public static async Task<Address> CreateAsync(string street, string district, string zipCode, int? number, string? additionalInformation, long cityId)
    {
        try
        {
            var address = new Address(street, district, zipCode, number, additionalInformation, cityId);

            var valitionResult = await _validator.ValidateAsync(address);

            if (!valitionResult.IsValid)
            {
                var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
                var errorsString = string.Join(", ", errors);
                throw new DelegaDomainException(errorsString);
            }

            return address;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
