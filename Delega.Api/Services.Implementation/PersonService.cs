using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Delega.Api.ViewModels;

namespace Delega.Api.Services.Implementation;

public class PersonService : IPersonService
{
    private readonly IPersonRepository repository;
    private readonly IUnitOfWork uow;

    public PersonService(IPersonRepository repository, IUnitOfWork uow)
    {
        this.repository = repository;
        this.uow = uow;
    }

    public Person Add(PersonCreateRequest personRequest)
    {
        try
        {
            var person = new Person
            {
                Cpf = personRequest.Cpf,
                CreatedTime = DateTime.Now,
                BirthDate = personRequest.BirthDate,
                FirstName = personRequest.FirstName,
                LastName = personRequest.LastName,
            };

            var entity = repository.Add(person);
            var result = uow.Commit();

            if (result is false)
                throw new Exception("Perosn cannot be inclued.");

            return entity;
        }
        catch (Exception) { throw; }
    }

    public void Delete(int id)
    {
        try
        {
            var person = GetById(id);

            if (person is null)
                throw new Exception("Person not found.");

            repository.Delete(person);

            var result = uow.Commit();

            if (result is false)
                throw new Exception("Perosn cannot be deleted.");

        }
        catch (Exception) { throw; }
    }

    public IEnumerable<Person> GetAll()
    {
        try
        {
            return repository.GetAll();
        }
        catch (Exception) { throw; }
    }

    public Person GetById(int id)
    {
        try
        {
            return repository.GetById(id);
        }
        catch (Exception) { throw; }
    }

    public Person Update(PersonViewModel person, int id)
    {
        throw new NotImplementedException();
    }

    private Person UpdatePersonInfos(PersonViewModel personViewModel, int id)
    {
        var personUpdate = repository.GetById(id);
        if (personUpdate is null)
            throw new Exception("Person not found.");

        if (!string.IsNullOrEmpty(personViewModel.Cpf))
            personUpdate.Cpf = personViewModel.Cpf;

        if (!string.IsNullOrEmpty(personViewModel.FirstName))
            personUpdate.FirstName = personViewModel.FirstName;

        if (!string.IsNullOrEmpty(personViewModel.LastName))
            personUpdate.LastName = personViewModel.LastName;

        if (personViewModel.BirthDate != DateTime.MinValue)
            personUpdate.BirthDate = personViewModel.BirthDate;

        personUpdate.UpadatedTime = DateTime.Now;

        return personUpdate;
    }
}
