using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;

namespace Delega.Infraestrutura.Services_Interfaces;

public interface IStateService
{
    Task<StateResponse> AddStateAsync(StateCreateDTO StateCad, CancellationToken cancellationToken);
    Task<StateResponse> UpdateStateAsync(StateUpdateDTO StateUpdate, CancellationToken cancellationToken);
    Task<StateResponse> GetStateAsync(long id, CancellationToken cancellationToken);
}
