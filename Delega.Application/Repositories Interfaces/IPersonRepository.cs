using Delega.Dominio.Entities;
using Delega.Infraestrutura.DTOs.Update;

namespace Delega.Infraestrutura.Repositories_Interfaces;

public interface IPersonRepository
{
    Task<Person> AddPersonAsync(Person person, CancellationToken cancellationToken);
    Task<Person> UpdatePersonAsync(Person personUpdate, CancellationToken cancellationToken);
    Task<Person> GetPersonAsync(long id, CancellationToken cancellationToken);
}
