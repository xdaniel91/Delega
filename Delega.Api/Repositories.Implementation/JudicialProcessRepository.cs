using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories.Implementation
{
    public class JudicialProcessRepository : IJudicialProcessRepository
    {
        private readonly DelegaContext Context;
        private readonly DbSet<JudicialProcess> DbSet;

        public JudicialProcessRepository(DelegaContext context)
        {
            Context = context;
            DbSet = Context.judicialprocesses;
        }

        public JudicialProcess Add(JudicialProcess JudicialProcess)
        {
            try
            {
                var entry = DbSet.Add(JudicialProcess);
                return entry.Entity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<JudicialProcessViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JudicialProcess> GetAllWithRelationships()
        {
            return DbSet.AsNoTracking();
        }

        //public JudicialProcess GetWithRelationships(int id)
        //{
        //    //return DbSet.FirstOrDefault(x => x.Id == id);
        //}
    }
}
