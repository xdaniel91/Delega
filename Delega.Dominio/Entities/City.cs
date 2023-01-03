using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Entities;

public class City : EntityBase
{
    public string Name { get; private set; }
    public State State { get; private set; }
    public long StateId { get; private set; }

    public City()
    {

    }

    public City(string name, long stateId)
    {
        Name = name;
        StateId = stateId;
        CreatedAt = DateTime.UtcNow;
    }

    private async Task<bool> ValidateAsync(CancellationToken cancellationToken)
    {
        var _cityValidator = new CityValidator();

        var valitionResult = await _cityValidator.ValidateAsync(this, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(", ", errors);
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
            if (name != null)
                Name = name;

            await ValidateAsync(cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }


    }
}
