using Delega.Api.Models;

namespace Delega.Api.Interfaces.Repositories;

public interface IPersonRepository
{
    Person Add(Person person);
    Task<Person> AddAsync(Person person, CancellationToken cancellationToken);
    void Delete(Person person);
    Person Update(Person person);
    Person GetById(int id);
    IEnumerable<Person> GetAll();
}

