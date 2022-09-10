namespace Delega.Api.Database
{
    public interface IUnitOfWork
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
