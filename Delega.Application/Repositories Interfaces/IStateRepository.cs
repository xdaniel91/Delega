using Delega.Dominio.Entities;

namespace Delega.Infraestrutura.Repositories_Interfaces;

public interface IStateRepository
{
    Task<State> AddStateAsync(State state, CancellationToken cancellationToken);
    Task<State> UpdateStateAsync(State stateUpdate, CancellationToken cancellationToken);
    Task<State> GetStateAsync(long id, CancellationToken cancellationToken);
}
