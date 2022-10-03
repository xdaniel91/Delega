using Delega.Api.Models;

namespace Delega.Api.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<Person> AddAsync(Person person, CancellationToken cancellationToken);
    Task<Person> GetByIdAsync(int id);
    Task<IEnumerable<Person>> GetAllAsync();
}

