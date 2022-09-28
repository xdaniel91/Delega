using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Delega.Api.Services.Implementation;
using Delega.Api.Utils;
using Delega.Api.Validators;
using FluentValidation;
using Moq;
using Xunit;


namespace Delega.Tests.Services;
public class PersonServiceTest
{
    private IPersonService personService;
    private IValidator<Person> _validator = new PersonValidator();

    public PersonServiceTest()
    {
        this.personService = new PersonService(
            new Mock<IPersonRepository>().Object,
            new Mock<IUnitOfWork>().Object);
    }

    #region AddAsync
    [Fact]
    public async Task PostAsync_InvalidFirstname()
    {
        var ex = await Assert.ThrowsAsync<DelegaException>(async () =>
           await personService.AddAsync(
           new PersonCreateRequest
           {
               FirstName = "",


               BirthDate = DateTime.Now.AddYears(-20),
               Cpf = "85406985019",
               LastName = "Haldoyrod"
           }, CancellationToken.None));

        var firstnameNotEmpty = ErrorMessagesSysid.FirstNameNotEmptySysid;
        var firstnameNotNull = ErrorMessagesSysid.FirstNameNotNullSysid;
        var firstnameLength = ErrorMessagesSysid.FirstNameMinimiumLengthSysid;

        var expectedMessage = ErrorMessages.GetMessageByLanguageSysid(firstnameNotEmpty);

        Assert.Contains(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task PostAsync_InvalidLasttname()
    {
        var ex = await Assert.ThrowsAsync<DelegaException>(async () =>
           await personService.AddAsync(
           new PersonCreateRequest
           {
               LastName = "",


               BirthDate = DateTime.Now.AddYears(-20),
               Cpf = "85406985019",
               FirstName = "Esnan"
           }, CancellationToken.None));

        var lastnameNotEmpty = ErrorMessagesSysid.LastNameNotEmptySysid;
        var lastnameNotNll = ErrorMessagesSysid.LastNameNotNullSysid;
        var lastnameLenght = ErrorMessagesSysid.LastNameMinimiumLengthSysid;

        var expectedMessage = ErrorMessages.GetMessageByLanguageSysid(lastnameNotEmpty);

        Assert.Contains(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task PostAsync_InvalidCpf()
    {
        var ex = await Assert.ThrowsAsync<DelegaException>(async () =>
           await personService.AddAsync(
           new PersonCreateRequest
           {
               Cpf = "",


               BirthDate = DateTime.Now.AddYears(-20),
               FirstName = "Esnan",
               LastName = "Haldoyrod"
           }, CancellationToken.None));

        var cpfNotEmpty = ErrorMessagesSysid.CpfNotEmptySysid;
        var cpfInvalid = ErrorMessagesSysid.CpfInvalidSysid;

        var expectedMessage = ErrorMessages.GetMessageByLanguageSysid(cpfNotEmpty);

        Assert.Contains(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task PostAsync_InvalidBirthdate()
    {
        var ex = await Assert.ThrowsAsync<DelegaException>(async () =>
           await personService.AddAsync(
           new PersonCreateRequest
           {
               BirthDate = DateTime.Now.AddYears(-115),


               Cpf = "85406985019",
               FirstName = "",
               LastName = "Haldoyrod"
           }, CancellationToken.None));

        var expectedMessage = ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.BirthDateInvalidSysid);

        Assert.Contains(expectedMessage, ex.Message);
    }
    #endregion

    #region Get

    [Fact]
    public void GetAll()
    {
        List<Person> _persons = new();

        _persons.Add(
            new Person
            {
                BirthDate = DateTime.Today,
                Cpf = "07685817101",
                CreatedTime = DateTime.UtcNow,
                FirstName = "Daniel",
                LastName = "Rodrigues",
                Id = 666
            });

        var _repository = new Mock<IPersonRepository>();
        _repository.Setup(x => x.GetAll()).Returns(_persons);

        this.personService = new PersonService(_repository.Object, new Mock<IUnitOfWork>().Object);

        var result = personService.GetAll();

        Assert.True(result.Count() > 0);
    }

    #endregion 
}
