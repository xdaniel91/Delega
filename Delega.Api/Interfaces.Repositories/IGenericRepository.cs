using Delega.Api.Models;

namespace Delega.Api.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : EntityBase
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Delete(T entity);
        bool Update(T entity);
        bool Add(T entity);
    }
}
