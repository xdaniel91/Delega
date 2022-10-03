using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Services.Implementation;
using Delega.Api.Services.Interfaces;
using Delega.Api.Utils;
using Moq;
using Xunit;

namespace Delega.Tests.Services;
public class PersonServiceTest
{
    private IPersonService personService;

    public PersonServiceTest()
    {
        personService = new PersonService(
            new Mock<IPersonRepository>().Object,
            new Mock<IUnitOfWork>().Object);
    }


    [Theory(DisplayName = "AddAsync fail")]
    [InlineData("Ka", "Anivia", "14429933081", "1999-02-25")]
    [InlineData("", "Anivia", "14429933081", "1999-02-05")]
    [InlineData(null, "Anivia", "14429933081", "1999-02-05")]
    [InlineData("Karthus", "An", "14429933081", "1999-02-05")]
    [InlineData("Karthus", "", "14429933081", "1999-02-05")]
    [InlineData("Karthus", null, "14429933081", "1999-02-05")]
    [InlineData("Karthus", "Anivia", "123456", "1999-02-05")]
    [InlineData("Karthus", "Anivia", "", "1999-02-05")]
    [InlineData("Karthus", "Anivia", null, "1999-02-05")]
    [InlineData("Karthus", "Anivia", "14429933081", "2020-02-05")]
    public async Task AddAsync_Fail(string firstname, string lastname, string cpf, string birth)
    {
        var ex = await Assert.ThrowsAsync<DelegaException>(async () =>
           await personService.AddAsync(
           new PersonCreateRequest
           {
               FirstName = firstname,
               Cpf = cpf,
               LastName = lastname,
               BirthDate = Convert.ToDateTime(birth)
           }, CancellationToken.None));

        var errorMessages = new string[] {
            ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.FirstNameMinimiumLengthSysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.FirstNameNotEmptySysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.FirstNameNotNullSysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LastNameMinimiumLengthSysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LastNameNotEmptySysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LastNameNotNullSysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.CpfInvalidSysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.CpfNotEmptySysid),
             ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.BirthDateInvalidSysid)};

        bool containsResult = false;

        foreach (var errorMessage in errorMessages)
        {
            if (ex.Message.Contains(errorMessage))
            {
                containsResult = true;
                break;
            }
        }

        Assert.True(containsResult);
    }

    [Fact(DisplayName = "AddAsync sucess")]
    public async Task AddAsync_SucessAsync()
    {
        var _personRequest = new PersonCreateRequest
        {
            BirthDate = DateTime.Now.AddYears(-30),
            Cpf = "07685817101",
            FirstName = "Udyr",
            LastName = "Rek'sai"
        };
        
        var _personInsert = new Person
        {
            BirthDate = DateTime.Now.AddYears(-30),
            Cpf = "07685817101",
            CreatedTime = DateTime.Today,
            FirstName = "Udyr",
            LastName = "Rek'sai",
            Id = 5,
            UpadatedTime = null
        };

        var result = await personService.AddAsync(_personRequest, CancellationToken.None);

        Assert.Null(result);
    }


    [Fact(DisplayName = "Get all")]
    public async Task GetAllAsync()
    {
        List<Person> _persons = new();
       
        _persons.Add(
            new Person
            {
                BirthDate = DateTime.Today,
                Cpf = "07685817101",
                CreatedTime = DateTime.UtcNow,
                FirstName = "Anivia",
                LastName = "Garen",
                Id = 666
            });

        var _repositoryMock = new Mock<IPersonRepository>();

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_persons);

        personService = new PersonService(_repositoryMock.Object, new Mock<IUnitOfWork>().Object);

        var result = await personService.GetAllAsync();

        Assert.True(result.Count() > 0);
    }

}
