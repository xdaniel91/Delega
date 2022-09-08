using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Delega.Api.Repositories.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DelegaContext Context;
        private readonly DbSet<Person> DbSet;
        public PersonRepository(DelegaContext context) : base(context)
        {
            Context = context;
            DbSet = this.Context.Set<Person>();
        }

        public bool Add(Person person)
        {
            DbSet.Add(person);
            return Context.SaveChanges() > 0;
        }

        public bool Delete(Person person)
        {
            DbSet.Remove(person);
            return Context.SaveChanges() > 0;
        }

        public IEnumerable<Person> GetAll()
        {
            return DbSet;
        }

        public Person GetById(int id)
        {
            return DbSet.FirstOrDefault(x => x.Id.Equals(id));
        }

        public bool Update(Person person)
        {
            DbSet.Update(person);
            return Context.SaveChanges() > 0;
        }
    }
}
