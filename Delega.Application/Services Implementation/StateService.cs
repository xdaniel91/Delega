using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Factories;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class StateService : IStateService
{
    protected readonly IStateRepository _stateRepository;
    protected readonly IUow _uow;

    public StateService(IStateRepository stateRepository, IUow uow)
    {
        _stateRepository = stateRepository;
        _uow = uow;
    }

    public async Task<StateResponse> AddStateAsync(StateCreateDTO stateCad, CancellationToken cancellationToken)
    {
        try
        {
            var stateInsert = await StateFactory.CreateAsync(stateCad.Name, stateCad.CountryId);
            var insertedState = await _stateRepository.AddStateAsync(stateInsert, cancellationToken);
            await _uow.CommitAsync(cancellationToken);

            return await GetStateAsync(insertedState.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<StateResponse> GetStateAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var state = await _stateRepository.GetStateAsync(id, cancellationToken);

            return new StateResponse
            {
                Country = state.Country.Name,
                CountryId = state.CountryId,
                Name = state.Name
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<StateResponse> UpdateStateAsync(StateUpdateDTO stateUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var state = await _stateRepository.GetStateAsync(stateUpdate.Id, cancellationToken, true);
            await state.UpdateAsync(stateUpdate.Name, cancellationToken);
            var updatedState = await _stateRepository.UpdateStateAsync(state, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetStateAsync(stateUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
