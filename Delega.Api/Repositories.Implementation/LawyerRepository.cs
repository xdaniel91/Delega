using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories.Implementation
{
    public class LawyerRepository : ILawyerRepository
    {
        private readonly DelegaContext Context;
        private readonly DbSet<Lawyer> DbSet;

        public LawyerRepository(DelegaContext context)
        {
            Context = context;
            DbSet = Context.lawyer;
        }

        public Lawyer Add(Lawyer lawyer)
        {
            try
            {
                var entry = DbSet.Add(lawyer);
                return entry.Entity;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Lawyer> GetAll()
        {
            return DbSet;
        }

        public Lawyer GetById(int id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public LawyerResponse GetResponse(int id)
        {
            var result = DbSet.Where(x => x.Id == id)
                .Include(x => x.Person)
                .FirstOrDefault();

            if (result is null)
                throw new Exception("Not found");

            return new LawyerResponse
            {
                Id = result.Id,
                Oab = result.Oab,
                PersonFirstName = result.Person.FirstName,
                PersonLastName = result.Person.LastName,
                PersonId = result.PersonId
            };

        }
    }
}
