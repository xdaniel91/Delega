using Delega.Api.Database;
using Delega.Api.Interfaces;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Repositories.Interfaces;
using Delega.Api.Services.Interfaces;
using Delega.Api.Utils;
using Delega.Api.Validators;
using FluentValidation;

namespace Delega.Api.Services.Implementation
{
    public class JudicialProcessService : IJudicialProcessService
    {
        private readonly IJudicialProcessRepository repository;
        private readonly IPersonRepository personRepository;
        private readonly ILawyerRepository lawyerRepositoy;
        private readonly IPersonService personService;
        private readonly IConsMessages consMessages;
        private readonly IUnitOfWork uow;
        


        public JudicialProcessService(IJudicialProcessRepository repository, IPersonRepository personRepository, ILawyerRepository lawyerRepositoy, IConsMessages consMessages, IUnitOfWork uow)
        {
            this.repository = repository;
            this.personRepository = personRepository;
            this.lawyerRepositoy = lawyerRepositoy;
            this.consMessages = consMessages;
            var language = Thread.CurrentThread.CurrentCulture.Name;
            consMessages.SetMessages(language);
            var errorMessages = consMessages.GetMessages();
            //Validator = new JudicialProcessValidator(errorMessages);
            this.uow = uow;
        }

        public JudicialProcess Add(JudicialProcessCreateRequest request)
        {

            //if (request.AuthorId == request.AccusedId)
            //    throw new Exception("Accused id cannot be equals author id.");

            //if (request.LawyerId == request.AuthorId || request.LawyerId == request.AccusedId)
            //    throw new Exception("Lawyer id cannot be equals author or accused id.");

            //var authorPerson = personRepository.GetById(request.AuthorId);
            //if (authorPerson is null)
            //    throw new Exception("Author not found.");

            //var accusedPerson = personRepository.GetById(request.AccusedId);
            //if (accusedPerson is null)
            //    throw new Exception("Accused not found.");

            //var lawyer = lawyerRepositoy.GetById(request.LawyerId);
            //if (lawyer is null)
            //    throw new Exception("Lawyer not found.");

            //var author = new Author
            //{
            //    CreatedTime = DateTime.Now,
            //    Depoiment = request.AuthorDepoiment,
            //    Person = authorPerson,
            //    PersonId = authorPerson.Id
            //};

            //var accused = new Accused
            //{
            //    CreatedTime = DateTime.Now,
            //    Person = accusedPerson,
            //    PersonId = accusedPerson.Id
            //};

            //var judicialProcess = new JudicialProcess
            //{
            //    Accused = accused,
            //    Author = author,
            //    Lawyer = lawyer,
            //    DateHourCreated = DateTime.Now,
            //    Reason = request.Reason,
            //    RequestedValue = request.RequestedValue,
            //    Status = (int)ConsGeneral.Status.Created
            //};

            //var validationResult = Validator.Validate(judicialProcess);

            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(sl => sl.ErrorMessage);
            //    var errorsString = string.Join(",", errors);
            //    throw new ValidationException($"Informações inconsistentes {Environment.NewLine}{errorsString}");
            //}

            //try
            //{
            //    var entity = repository.Add(judicialProcess);
            //    var result = uow.Commit();
            //    return entity;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return null;
        }

        public IEnumerable<JudicialProcess> GetAllWithRelationships()
        {
            return repository.GetAllWithRelationships();
        }

        public JudicialProcess GetByIdWithRelationships(int id)
        {
            //if (id <= 0)
            //    throw new Exception("Invalid id.");

            //return repository.GetWithRelationships(id);
            return null;
        }
    }
}
