using Delega.Application.Repositories_Interfaces;
using Delega.Infraestrutura.Database;

namespace Delega.Infraestrutura.Repositories_Implementation;

public class Uow : IUow
{
    protected readonly DelegaContext Context;

    public Uow(DelegaContext dbContext)
    {
        Context = dbContext;
            
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}
