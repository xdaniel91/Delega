using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Entities;

public sealed class Person : EntityBase
{


    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Cpf { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Address Address { get; private set; }
    public long AddressId { get; private set; }
    public int Age { get; private set; }

    public Person()
    {
        //empty constructor for entityframework.
    }

    public Person(string firstname, string lastname, string cpf, DateTime birthdate, long addressId)
    {
        int age = DateTime.Today.Year - BirthDate.Year;
        if (BirthDate < DateTime.Today)
            age--;

        FirstName = firstname;
        LastName = lastname;
        Cpf = cpf;
        BirthDate = birthdate;
        AddressId = addressId;
        CreatedAt = DateTime.UtcNow;
        Age = age;
    }

    private async Task<bool> ValidateAsync(CancellationToken cancellationToken)
    {
        var _personValidator = new PersonValidator();
        var valitionResult = await _personValidator.ValidateAsync(this, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(", ", errors);
            throw new DelegaDomainException(errorsString);
        }

        return true;
    }

    public async Task UpdateAsync(string? firstName, string? lastName, string? cpf, DateTime? birth, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new OperationCanceledException("Operação cancelada");

        try
        {
            if (firstName != null)
                FirstName = firstName;

            if (lastName != null)
                LastName = lastName;

            if (cpf != null)
                Cpf = cpf;

            if (birth != null)
                BirthDate = birth.Value;

            await ValidateAsync(cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
