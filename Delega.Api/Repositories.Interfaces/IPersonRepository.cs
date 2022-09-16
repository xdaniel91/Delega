using Delega.Api.Models;

namespace Delega.Api.Interfaces.Repositories;

public interface IPersonRepository
{
    Person Add(Person person);
    void Delete(Person person);
    Person Update(Person person);
    Person GetById(int id);
    IEnumerable<Person> GetAll();
}

