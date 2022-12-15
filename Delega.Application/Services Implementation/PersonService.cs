using Delega.Application.Exceptions;
using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Entities;
using Delega.Dominio.Validators;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class PersonService : IPersonService
{
    private readonly PersonValidator _personValidator = new PersonValidator();
    protected readonly IPersonRepository _personRepository;
    protected readonly IUow _uow;

    public PersonService(IPersonRepository personRepository, IUow uow)
    {
        _personRepository = personRepository;
        _uow = uow;
    }

    public async Task<PersonResponse> AddPersonAsync(PersonCreateDTO personCad, CancellationToken cancellationToken)
    {
        try
        {
            var personInsert = new Person(personCad.FirstName, personCad.LastName, personCad.Cpf, personCad.BirthDate, personCad.AddressId);
            await ValidarAsync(personInsert, cancellationToken);
            var insertedPerson = await _personRepository.AddPersonAsync(personInsert, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);
         
            return new PersonResponse
            {
                AddressId = personInsert.AddressId,
                BirthDate = personInsert.BirthDate,
                Cpf = personInsert.Cpf,
                FirstName = personInsert.FirstName,
                LastName = personInsert.LastName
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PersonResponse> GetPersonAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var person = await _personRepository.GetPersonAsync(id, cancellationToken);

            return new PersonResponse
            {
                AddressId = person.AddressId,
                BirthDate = person.BirthDate,
                Cpf = person.Cpf,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PersonResponse> UpdatePersonAsync(PersonUpdateDTO personUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var person = await _personRepository.GetPersonAsync(personUpdate.Id, cancellationToken);

            if (personUpdate.FirstName != null)
                person.FirstName = personUpdate.FirstName;

            if (personUpdate.LastName != null)
                person.LastName = personUpdate.LastName;

            if (personUpdate.Cpf != null)
                person.Cpf = personUpdate.Cpf;

            if (personUpdate.BirthDate != null)
                person.BirthDate = personUpdate.BirthDate.Value;

            await ValidarAsync(person, cancellationToken);

            var updatedPerson = await _personRepository.UpdatePersonAsync(person, cancellationToken);

            var result = await _uow.CommitAsync(cancellationToken);

            return new PersonResponse
            {
                BirthDate = updatedPerson.BirthDate,
                Cpf = updatedPerson.Cpf,
                AddressId = updatedPerson.AddressId,
                FirstName = updatedPerson.FirstName,
                LastName = updatedPerson.LastName,
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ValidarAsync(Person person, CancellationToken cancellationToken)
    {
        var valitionResult = await _personValidator.ValidateAsync(person, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(", ", errors);
            throw new DelegaApplicationException(errorsString);
        }
    }
}
