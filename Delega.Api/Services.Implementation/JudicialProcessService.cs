using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Models.ViewModels;
using Delega.Api.Utils;
using Delega.Api.Validators;
using FluentValidation;

namespace Delega.Api.Services.Implementation;

public class JudicialProcessService : IJudicialProcessService
{
    private readonly IJudicialProcessRepository repository;
    private readonly IPersonRepository personRepository;
    private readonly ILawyerRepository lawyerRepositoy;
    private readonly IValidator<JudicialProcess> Validator = new JudicialProcessValidator();
    private readonly IUnitOfWork uow;

    public JudicialProcessService(
        IJudicialProcessRepository repository,
        IPersonRepository personRepository,
        ILawyerRepository lawyerRepositoy,
        IUnitOfWork uow)
    {
        this.repository = repository;
        this.personRepository = personRepository;
        this.lawyerRepositoy = lawyerRepositoy;
        this.uow = uow;
    }

    public async Task<JudicialProcessViewModel> AddAsync(JudicialProcessCreateRequest request)
    {

        if (request.AuthorId == request.AccusedId)
            throw new DelegaException("Accused id cannot be equals author id.");

        if (request.LawyerId == request.AuthorId || request.LawyerId == request.AccusedId)
            throw new DelegaException("Lawyer id cannot be equals author or accused id.");

        var authorPerson = personRepository.GetById(request.AuthorId);
        if (authorPerson is null)
            throw new DelegaException("Author not found.");

        var accusedPerson = personRepository.GetById(request.AccusedId);
        if (accusedPerson is null)
            throw new DelegaException("Accused not found.");

        var lawyer = lawyerRepositoy.GetById(request.LawyerId);
        if (lawyer is null)
            throw new DelegaException("Lawyer not found.");

        var author = new Author
        {
            CreatedTime = DateTime.Now,
            Depoiment = request.AuthorDepoiment,
            PersonId = authorPerson.Id,
            Cpf = authorPerson.Cpf,
            Name = $"{authorPerson.FirstName} + {authorPerson.LastName}"
        };

        var accused = new Accused
        {
            CreatedTime = DateTime.Now,
            PersonId = accusedPerson.Id,
            Cpf = accusedPerson.Cpf,
            Name = $"{accusedPerson.FirstName} + {accusedPerson.LastName}"
        };

        var judicialProcess = new JudicialProcess
        {
            Accused = accused,
            Author = author,
            Lawyer = lawyer,
            DateHourCreated = DateTime.Now,
            Reason = request.Reason,
            RequestedValue = request.RequestedValue,
            Status = (int)ConstGeneral.StatusJudicialProcess.Created
        };

        var validationResult = Validator.Validate(judicialProcess);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(sl => sl.ErrorMessage);
            var errorsString = string.Join(",", errors);
            throw new DelegaException($"Informações inconsistentes {Environment.NewLine}{errorsString}");
        }

        try
        {
            var entity = await repository.AddAsync(judicialProcess);

            var result = uow.Commit();

            return repository.GetResponse(entity.Id);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<JudicialProcess>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<JudicialProcessViewModel> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public JudicialProcessViewModel GetViewModel(int id)
    {
        return repository.GetResponse(id);
    }

    public Task<JudicialProcess> GetWithRelationsAsync(int id)
    {
        return repository.GetWithRelationsAsync(id);
    }

    public async Task<JudicialProcess> InProgressAsync(int id)
    {
        var process = await GetWithRelationsAsync(id);

        if (process is null)
            throw new DelegaException("judicial process not found");

        if (process.Status != ConstGeneral.StatusJudicialProcess.Created)
            throw new DelegaException("just can set 'InProgress' status for processes with 'Created' status.");

        process.Status = ConstGeneral.StatusJudicialProcess.InProgress;
        process.DateHourInProgress = DateTime.UtcNow;
        var entity = repository.Update(process);
        var result = uow.Commit();

        if (result is false)
            throw new DelegaException("cannot possible update judicial process.");

        return entity;
    }
}

