using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Entities;

public class Country : EntityBase
{
    public string Name { get; set; }

    public Country(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public Country()
    {

    }

    private async Task<bool> ValidateAsync(CancellationToken cancellationToken)
    {
        var _countryValidator = new CountryValidator();

        var valitionResult = await _countryValidator.ValidateAsync(this, cancellationToken);

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
