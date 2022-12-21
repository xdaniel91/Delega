using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Factories;

public static class PersonFactory
{
    private static readonly PersonValidator _validator = new PersonValidator();

    public static async Task<Person> CreateAsync(string firstname, string lastname, string cpf, DateTime birthdate, long addressId)
    {
        try
        {
            var person = new Person(firstname, lastname, cpf, birthdate, addressId);

            var valitionResult = await _validator.ValidateAsync(person);

            if (!valitionResult.IsValid)
            {
                var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
                var errorsString = string.Join(", ", errors);
                throw new DelegaDomainException(errorsString);
            }

            return person;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
