using Delega.Application.Exceptions;
using Delega.Dominio.Entities;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Repositories_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delega.Infraestrutura.Repositories_Implementation;

public class StateRepository : IStateRepository
{
    protected readonly DelegaContext Context;
    protected readonly DbSet<State> States;

    public StateRepository(DelegaContext dbContext)
    {
        Context = dbContext;
        States = Context.state;
    }

    public async Task<State> AddStateAsync(State state, CancellationToken cancellationToken)
    {
        try
        {
            var result = await States.AddAsync(state, cancellationToken);
            return result.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<State> GetStateAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var state = await States.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return state is null ? throw new DelegaDataException("State not found") : state;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<State> UpdateStateAsync(State state, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new DelegaDataException("Request cancelled");

        try
        {
            var result = States.Update(state);

            return result.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
