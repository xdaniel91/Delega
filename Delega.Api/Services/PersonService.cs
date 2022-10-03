using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Services.Interfaces;
using Delega.Api.Validators;
using FluentValidation;
using System.Threading;

namespace Delega.Api.Services.Implementation;

public class PersonService : IPersonService
{
    private readonly IValidator<Person> Validator;

    private readonly IPersonRepository repository;
    private readonly IUnitOfWork uow;

    public PersonService(IPersonRepository repository, IUnitOfWork uow)
    {
        this.repository = repository;
        this.uow = uow;

        Validator = new PersonValidator();
    }

    public async Task<Person> AddAsync(PersonCreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var person = await CreateNewPersonAsync(request);
            var entity = await repository.AddAsync(person, cancellationToken);
            var result = await uow.CommitAsync(cancellationToken);
           
            return entity;
        }
        catch (DelegaException ex)
        {
            throw ex;
        }
        catch (TaskCanceledException ex)
        {
            throw ex;
        }
        catch (OperationCanceledException ex)
        {
            throw ex;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Person> CreateNewPersonAsync(PersonCreateRequest request)
    {
        var person = new Person
        {
            Cpf = request.Cpf,
            CreatedTime = DateTime.Now,
            BirthDate = request.BirthDate,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var validationResult = await Validator.ValidateAsync(person, CancellationToken.None);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errosString = string.Join(",", erros);
            throw new DelegaException($"Informações inconsistentes.{Environment.NewLine}{errosString}");
        }
        else
            return person;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        try
        {
            return await repository.GetAllAsync();
        }
        catch (Exception) { throw; }
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        try
        {
            return await repository.GetByIdAsync(id);
        }
        catch (Exception) { throw; }
    }
}
