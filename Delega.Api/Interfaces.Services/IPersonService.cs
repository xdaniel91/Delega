using Delega.Api.Models;
using Delega.Api.ViewModels;

namespace Delega.Api.Interfaces.Services
{
    public interface IPersonService
    {
        public bool Add(Person person);
        public bool Delete(Person person);
        public bool Update(PersonViewModel person, int id);
        public IEnumerable<Person> GetAll();
        public Person GetById(int id);
    }
}
