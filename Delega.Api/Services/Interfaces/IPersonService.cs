using Delega.Api.Models;

namespace Delega.Api.Services.Interfaces;

public interface IPersonService
{
    public Task<Person> AddAsync(PersonCreateRequest person, CancellationToken cancellationToken);
    public Task<IEnumerable<Person>> GetAllAsync();
    public Task<Person> GetByIdAsync(int id);
    public Task<Person> CreateNewPersonAsync(PersonCreateRequest request);
}
