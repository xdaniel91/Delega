using Autofac.Extras.Moq;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Exceptions;
using Delega.Infraestrutura.Interfaces.Repositories;
using Delega.Infraestrutura.Services.Implementation;
using Delega.Infraestrutura.Services.Interfaces;
using Delega.Infraestrutura.Utils;
using Delega.Dominio.Entities;
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


    [Theory(DisplayName = "CreateNewPerson() throw an DelegaException")]
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
    public async Task CreateNewPerson_ThrowDelegaException(string firstname, string lastname, string cpf, string birth)
    {
        var ex = await Assert.ThrowsAsync<DelegaException>(async () =>
           await personService.CreateNewPersonAsync(
           new PersonCreateRequest
           {
               FirstName = firstname,
               Cpf = cpf,
               LastName = lastname,
               BirthDate = Convert.ToDateTime(birth)
           }));

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


    [Fact(DisplayName = "CreateNewPerson() sucess")]
    public async void CreateNewPerson_Sucess()
    {
        var _request = new PersonCreateRequest
        {
            BirthDate = DateTime.Now.AddYears(-30),
            Cpf = "07685817101",
            FirstName = "Galio",
            LastName = "Aatrox"
        };

        var _infos = new string[]
        {
             _request.FirstName,
             _request.LastName,
             _request.Cpf,
             _request.BirthDate.ToString()
        };

        var result = await personService.CreateNewPersonAsync(_request);

        var isEqual = _infos[0] == result.FirstName &&
                        _infos[1] == result.LastName &&
                        _infos[2] == result.Cpf &&
                        _infos[3] == result.BirthDate.ToString();

        Assert.True(isEqual);
    }

    [Fact]
    public async Task GetAllAsync_SucessAsync()
    {
        var _people = new Person[] {

            new Person
            {
                Id = 1,
                FirstName = "Galio",
                LastName = "Aatrox",
                Cpf = "07685817101",
                BirthDate = DateTime.Now.AddYears(-25),
                CreatedTime = DateTime.Now,
                UpadatedTime = null
            },
            new Person
            {
                Id = 2,
                FirstName = "Akali",
                LastName = "Darius",
                Cpf = "07685817101",
                BirthDate = DateTime.Now.AddYears(-25),
                CreatedTime = DateTime.Now,
                UpadatedTime = null
            },
            new Person
            {
                Id = 3,
                FirstName = "Irelia",
                LastName = "Ashe",
                Cpf = "07685817101",
                BirthDate = DateTime.Now.AddYears(-25),
                CreatedTime = DateTime.Now,
                UpadatedTime = null
            },
            new Person
            {
                Id = 4,
                FirstName = "Sejuani",
                LastName = "Lissandra",
                Cpf = "07685817101",
                BirthDate = DateTime.Now.AddYears(-25),
                CreatedTime = DateTime.Now,
                UpadatedTime = null
            },
        };

        var _personRepMock = new Mock<IPersonRepository>();
        _personRepMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_people);
        
        personService = new PersonService(
            _personRepMock.Object, 
            new Mock<IUnitOfWork>().Object);

        var result = await personService.GetAllAsync();
        bool isEqual = result == _people; 

        Assert.True(isEqual);
    }


    [Fact(DisplayName = "AddAsync sucess", Skip = "This test is broken")]
    public async Task AddAsync_SucessAsync()
    {
        using (var mock = AutoMock.GetLoose())
        {

        }
    }

    private Person GetPerson()
    {
        return new Person
        {
            Id = 1,
            BirthDate = DateTime.Now.AddYears(-20),
            Cpf = "07685817101",
            CreatedTime = DateTime.Now,
            FirstName = "Miss",
            LastName = "Fortune",
            UpadatedTime = null
        };
    }
}
