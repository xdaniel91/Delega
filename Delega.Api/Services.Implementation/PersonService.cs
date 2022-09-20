using Delega.Api.Database;
using Delega.Api.Interfaces;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Delega.Api.Validators;
using Delega.Api.ViewModels;
using FluentValidation;
using System.Globalization;

namespace Delega.Api.Services.Implementation;

public class PersonService : IPersonService
{
    private readonly static string _idioma = Thread.CurrentThread.CurrentCulture.Name;
    private readonly IValidator<Person> Validator = new PersonValidator(_idioma);
   
    private readonly IPersonRepository repository;
    private readonly IUnitOfWork uow;
    private readonly IConsMessages ConsMessages;

    public PersonService(IPersonRepository repository, IUnitOfWork uow, IConsMessages messages)
    {
        this.repository = repository;
        this.uow = uow;
        //var language = Thread.CurrentThread.CurrentCulture.Name;
        //var language = CultureInfo.CreateSpecificCulture("en-US");
        messages.SetMessages(_idioma);
        //Messages = messages.GetMessages();
        ConsMessages = messages;

    }

    public Person Add(PersonCreateRequest personRequest)
    {

        var person = new Person
        {
            Cpf = personRequest.Cpf,
            CreatedTime = DateTime.Now,
            BirthDate = personRequest.BirthDate,
            FirstName = personRequest.FirstName,
            LastName = personRequest.LastName,
        };

        var validationResult = Validator.Validate(person);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errosString = string.Join(",", erros);
            throw new ValidationException($"Informações inconsistentes.{Environment.NewLine}{errosString}");
        }

        try
        {
            var entity = repository.Add(person);
            var result = uow.Commit();
            return entity;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Delete(int id)
    {
        try
        {
            var person = GetById(id);

            if (person is null)
                throw new Exception("Person not found.");

            repository.Delete(person);

            var result = uow.Commit();

            if (result is false)
                throw new Exception("Perosn cannot be deleted.");

        }
        catch (Exception) { throw; }
    }

    public IEnumerable<Person> GetAll()
    {
        try
        {
            return repository.GetAll();
        }
        catch (Exception) { throw; }
    }

    public Person GetById(int id)
    {
        try
        {
            return repository.GetById(id);
        }
        catch (Exception) { throw; }
    }

    public Person Update(PersonViewModel person, int id)
    {
        throw new NotImplementedException();
    }

    private Person UpdatePersonInfos(PersonViewModel personViewModel, int id)
    {
        var personUpdate = repository.GetById(id);
        if (personUpdate is null)
            throw new Exception("Person not found.");

        if (!string.IsNullOrEmpty(personViewModel.Cpf))
            personUpdate.Cpf = personViewModel.Cpf;

        if (!string.IsNullOrEmpty(personViewModel.FirstName))
            personUpdate.FirstName = personViewModel.FirstName;

        if (!string.IsNullOrEmpty(personViewModel.LastName))
            personUpdate.LastName = personViewModel.LastName;

        if (personViewModel.BirthDate != DateTime.MinValue)
            personUpdate.BirthDate = personViewModel.BirthDate;

        personUpdate.UpadatedTime = DateTime.Now;

        return personUpdate;
    }
}
