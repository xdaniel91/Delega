using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Factories;
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
            var personInsert = await PersonFactory.CreateAsync(personCad.FirstName, personCad.LastName, personCad.Cpf, personCad.BirthDate, personCad.AddressId);
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
            var person = await _personRepository.GetPersonAsync(personUpdate.Id, cancellationToken, true);

            await person.UpdateAsync(personUpdate.FirstName, personUpdate.LastName, personUpdate.Cpf, personUpdate.BirthDate, cancellationToken);     
            var updatedPerson = await _personRepository.UpdatePersonAsync(person, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetPersonAsync(personUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
