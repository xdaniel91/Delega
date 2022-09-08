using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Delega.Api.ViewModels;

namespace Delega.Api.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository repository;

        public PersonService(IPersonRepository repository)
        {
            this.repository = repository;
        }

        public bool Add(Person person)
        {
            return repository.Add(person);
        }

        public bool Delete(Person person)
        {
            return repository.Delete(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return repository.GetAll();
        }

        public Person GetById(int id)
        {
            return repository.GetById(id);
        }

        public bool Update(PersonViewModel person, int id)
        {
            return repository.Update(UpdatePersonInfos(person, id));
        }

        private Person UpdatePersonInfos(PersonViewModel personViewModel, int id)
        {
            var personUpdate = repository.GetById(id);
            if (personUpdate is null)
                throw new Exception("Person not found.");

            if (!string.IsNullOrEmpty(personViewModel.Cpf))
                personUpdate.Cpf = personViewModel.Cpf;

            if(!string.IsNullOrEmpty(personViewModel.FirstName))
                personUpdate.FirstName = personViewModel.FirstName;

            if (!string.IsNullOrEmpty(personViewModel.LastName))
                personUpdate.LastName = personViewModel.LastName;

            if (personViewModel.BirthDate != DateTime.MinValue)
                personUpdate.BirthDate = personViewModel.BirthDate;

            personUpdate.UpadatedTime = DateTime.Now;

            return personUpdate;
        }
    }
}
