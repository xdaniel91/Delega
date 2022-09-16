using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;

namespace Delega.Api.Services.Implementation
{
    public class LawyerService : ILawyerService
    {
        private readonly ILawyerRepository repository;
        private readonly IPersonRepository personRepository;
        private readonly IUnitOfWork uow;

        public LawyerService(ILawyerRepository repository, IPersonRepository personRepository, IUnitOfWork uow)
        {
            this.repository = repository;
            this.personRepository = personRepository;
            this.uow = uow;
        }

        public LawyerResponse Add(LawyerCreateRequest lawyerCreateRequest)
        {
            if (lawyerCreateRequest.PersonId <= 0)
                throw new Exception("The person id was not informed.");

            var lawyer = new Lawyer
            {
                Oab = lawyerCreateRequest.Oab,
                PersonId = lawyerCreateRequest.PersonId
            };

            var result = repository.Add(lawyer);

            var commitResult = uow.Commit();

            return GetResponse(result.Id);
        }

        public IEnumerable<Lawyer> GetAll()
        {
            var lawyers = repository.GetAll();

            return lawyers; 
        }

        public Lawyer GetById(int id)
        {
            var lawyer = repository.GetById(id);

            return lawyer;
        }

        public LawyerResponse GetResponse(int id)
        {
            return repository.GetResponse(id);
        }
    }
}
