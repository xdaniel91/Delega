using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Services.Interfaces;

namespace Delega.Api.Services.Implementation;

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

    public async Task<Lawyer> AddAsync(LawyerCreateRequest lawyerCreateRequest)
    {
        if (lawyerCreateRequest.PersonId <= 0)
            throw new DelegaException("Invalid person id.");

        var person = await personRepository.GetByIdAsync(lawyerCreateRequest.PersonId);

        if (person is null)
            throw new DelegaException("Person not found.");

        var lawyer = new Lawyer
        {
            Oab = lawyerCreateRequest.Oab,
            PersonId = lawyerCreateRequest.PersonId,
            Name = $"{person.FirstName} + {person.LastName}",
            CreatedTime = DateTime.UtcNow
        };

        var result = repository.Add(lawyer);

        var commitResult = uow.Commit();
       
        return result;
    }

    public IEnumerable<Lawyer> GetAll()
    {
        var lawyers = repository.GetAll();

        return lawyers;
    }

    public Task<Lawyer> GetByIdAsync(int id)
    {
        var lawyer = repository.GetByIdAsync(id);

        return lawyer;
    }

    public LawyerResponse GetResponse(int id)
    {
        return repository.GetResponse(id);
    }
}
