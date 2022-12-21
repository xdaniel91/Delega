using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Factories;

public static class StateFactory
{
    private static readonly StateValidator _validator = new StateValidator();

    public static async Task<State> CreateAsync(string name, long countryId)
    {
        try
        {
            var state = new State(name, countryId);

            var valitionResult = await _validator.ValidateAsync(state);

            if (!valitionResult.IsValid)
            {
                var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
                var errorsString = string.Join(", ", errors);
                throw new DelegaDomainException(errorsString);
            }

            return state;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
