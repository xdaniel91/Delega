using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Database
{
    public class DelegaContext : DbContext
    {
        public DelegaContext(DbContextOptions<DelegaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
