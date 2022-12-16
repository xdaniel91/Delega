using Delega.Application.Exceptions;
using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Entities;
using Delega.Dominio.Validators;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class StateService : IStateService
{
    private readonly StateValidator _stateValidator = new StateValidator();
    protected readonly IStateRepository _addressRepository;
    protected readonly IUow _uow;

    public StateService(IStateRepository addressRepository, IUow uow)
    {
        _addressRepository = addressRepository;
        _uow = uow;
    }

    public async Task<StateResponse> AddStateAsync(StateCreateDTO stateCad, CancellationToken cancellationToken)
    {
        try
        {
            var stateInsert = new State(stateCad.Name, stateCad.CountryId);
            await ValidarAsync(stateInsert, cancellationToken);
            var insertedState = await _addressRepository.AddStateAsync(stateInsert, cancellationToken);
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
            var state = await _addressRepository.GetStateAsync(id, cancellationToken);

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
            var state = await _addressRepository.GetStateAsync(stateUpdate.Id, cancellationToken, true);

            if (stateUpdate.Name != null)
                state.Name = stateUpdate.Name;

            await ValidarAsync(state, cancellationToken);

            var updatedState = await _addressRepository.UpdateStateAsync(state, cancellationToken);

            var result = await _uow.CommitAsync(cancellationToken);

            return await GetStateAsync(stateUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ValidarAsync(State state, CancellationToken cancellationToken)
    {
        var valitionResult = await _stateValidator.ValidateAsync(state, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(",", errors);
            throw new DelegaApplicationException(errorsString);
        }
    }
}
