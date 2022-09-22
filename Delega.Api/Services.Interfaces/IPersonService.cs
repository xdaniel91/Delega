using Delega.Api.Models;
using Delega.Api.ViewModels;

namespace Delega.Api.Interfaces.Services;

public interface IPersonService
{
    public Person Add(PersonCreateRequest person);
    public Task<Person> AddAsync(PersonCreateRequest person, CancellationToken cancellationToken);
    public void Delete(int id);
    public Person Update(PersonViewModel person, int id);
    public IEnumerable<Person> GetAll();
    public Person GetById(int id);
}
