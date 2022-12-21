using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Entities;

public class State : EntityBase
{
    public string Name { get; private set; }
    public Country Country { get; private set; }
    public long CountryId { get; private set; }

    public State(string name, long countryId)
    {
        Name = name;
        CountryId = countryId;
        CreatedAt = DateTime.UtcNow;
    }

    public State()
    {

    }

    private async Task<bool> ValidateAsync(CancellationToken cancellationToken)
    {
        var _stateValidator = new StateValidator();

        var valitionResult = await _stateValidator.ValidateAsync(this, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(",", errors);
            throw new DelegaDomainException(errorsString);
        }

        return true;
    }

    public async Task UpdateAsync(string? name, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new OperationCanceledException("Operação cancelada");

        try
        {
            if (name is not null)
                Name = name;

            await ValidateAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
