namespace Delega.Application.Repositories_Interfaces;

public interface IUow
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
