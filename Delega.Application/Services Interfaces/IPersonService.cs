using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;

namespace Delega.Infraestrutura.Services_Interfaces;

public interface IPersonService
{
    Task<PersonResponse> AddPersonAsync(PersonCreateDTO personCad, CancellationToken cancellationToken);
    Task<PersonResponse> UpdatePersonAsync(PersonUpdateDTO personUpdate, CancellationToken cancellationToken);
    Task<PersonResponse> GetPersonAsync(long id, CancellationToken cancellationToken);
}
