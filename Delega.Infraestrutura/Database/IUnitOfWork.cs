namespace Delega.Infraestrutura.Database
{
    public interface IUnitOfWork
    {
        bool Commit();
        Task<bool> CommitAsync(CancellationToken ct);
    }
}
