using Delega.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository
    {
        private readonly DbContext _context;

        private readonly DbSet<T> entities;

        public GenericRepository(DbContext dbContext)
        {
            _context = dbContext;
            entities = _context.Set<T>();
        }
    }
}
