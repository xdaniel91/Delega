using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Services.Implementation;
using Delega.Api.Services.Interfaces;
using Moq;
using Xunit;
using static Delega.Api.Utils.ConstGeneral;

namespace Delega.Tests.Services;

public class JudicialProcessServiceTest
{
    IJudicialProcessService _judicialProcessService;

    public JudicialProcessServiceTest()
    {
        _judicialProcessService = new JudicialProcessService(
            new Mock<IJudicialProcessRepository>().Object,
            new Mock<IPersonRepository>().Object,
            new Mock<ILawyerRepository>().Object,
            new Mock<IUnitOfWork>().Object);
    }

    [Fact(DisplayName = "CreateNewAuthor() Sucess")]
    public async Task CreateNewAuthor_Sucess()
    {
        var _person = new Person
        {
            Id = 2,
            BirthDate = DateTime.Now.AddYears(-25),
            Cpf = "07685817101",
            CreatedTime = DateTime.Now,
            FirstName = "Amumu",
            LastName = "Lulu",
            UpadatedTime = null
        };

        var _request = new JudicialProcessCreateRequest
        {
            AuthorId = 2,
            AccusedId = 3,
            LawyerId = 1,
            Reason = "cheat",
        };

        var personRepMock = new Mock<IPersonRepository>();
        personRepMock.Setup(x => x.GetByIdAsync(_person.Id)).ReturnsAsync(_person);

        _judicialProcessService = new JudicialProcessService(
            new Mock<IJudicialProcessRepository>().Object,
            personRepMock.Object,
            new Mock<ILawyerRepository>().Object,
            new Mock<IUnitOfWork>().Object);

        var author = await _judicialProcessService.CreateNewAuthorAsync(_request);

        var expected = _person.Id;
        Assert.Equal(expected, author.PersonId);
    }

    [Fact(DisplayName = "CreateNewAuthor() Fail when author id accused id is equal")]
    public async Task CreateNewAuthor_Fails_WhenAccusedIdIsEqualAsync()
    {
        //arrange
        var _person = new Person
        {
            Id = 2,
            BirthDate = DateTime.Now.AddYears(-25),
            Cpf = "07685817101",
            CreatedTime = DateTime.Now,
            FirstName = "Amumu",
            LastName = "Lulu",
            UpadatedTime = null
        };

        var _request = new JudicialProcessCreateRequest
        {
            AuthorId = 2,
            AccusedId = 2,
            LawyerId = 1,
            Reason = "cheat",
        };

        //act
        var ex = await Assert.ThrowsAsync<DelegaException>(async () => await _judicialProcessService.CreateNewAuthorAsync(_request));
        var expected = "Accused id cannot be equals author id.";
        var actual = ex.Message;

        //assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CreateNewAccused sucess")]
    public async Task CreateNewAccused_Sucess()
    {
        //arrange
        var _request = new JudicialProcessCreateRequest
        {
            AccusedId = 1,
            AuthorId = 3,
            AuthorDepoiment = "my girl cheat me",
            LawyerId = 1,
            Reason = "cheat",
            RequestedValue = 1988.66M
        };

        var _person = new Person
        {
            Cpf = "07685817101",
            FirstName = "Aatrox",
            LastName = "Sad",
            BirthDate = DateTime.Now.AddYears(-25),
            CreatedTime = DateTime.Now,
            Id = 10,
            UpadatedTime = null
        };

        var personRepMock = new Mock<IPersonRepository>();
        personRepMock.Setup(x => x.GetByIdAsync(_person.Id)).ReturnsAsync(_person);

        _judicialProcessService = new JudicialProcessService(
            new Mock<IJudicialProcessRepository>().Object,
            personRepMock.Object,
            new Mock<ILawyerRepository>().Object,
            new Mock<IUnitOfWork>().Object);

        //act
        var accused = await _judicialProcessService.CreateNewAccusedAsync(_request);

        //assert
        var expected = _person.Id;
        Assert.Equal(expected, accused.PersonId);
    }

    [Fact(DisplayName = "CreateNewAccused fails when author id is equal accused id.")]
    public async Task CreateNewAccused_Fail_WhenAuthorIdIsEqualAsync()
    {
        //arrange
        var _request = GetRequest();
        _request.AccusedId = _request.AuthorId = 5;

        //act
        var ex = await Assert.ThrowsAsync<DelegaException>(async () => await _judicialProcessService.CreateNewAccusedAsync(_request));
        var expected = "Accused id cannot be equals author id.";
        var actual = ex.Message;

        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateNewJudicialProcess_Sucess()
    {
        //arrange
        var _request = GetRequest();
        var _accused = GetAccused();
        var _author = GetAuthor();
        var _lawyer = GetLawyer();

        var personRepMock = new Mock<IPersonRepository>();
        personRepMock.Setup(x => x.GetByIdAsync(_accused.PersonId)).ReturnsAsync(GetPerson());

        var lawyerRepMock = new Mock<ILawyerRepository>();
        lawyerRepMock.Setup(x => x.GetByIdAsync(_lawyer.Id)).ReturnsAsync(_lawyer);

        var serviceMock = new Mock<IJudicialProcessService>();
        serviceMock.Setup(x => x.CreateNewAccusedAsync(_request)).ReturnsAsync(GetAccused());
        serviceMock.Setup(x => x.CreateNewAuthorAsync(_request)).ReturnsAsync(GetAuthor());

        var _service = new Mock<JudicialProcessService>(
            new Mock<IJudicialProcessRepository>().Object,
            personRepMock.Object,
            lawyerRepMock.Object,
            new Mock<IUnitOfWork>().Object).Object;

        //act
        var actual = await _service.CreateNewJudicialProcessAsync(_request);
        var exptectedStatus = StatusJudicialProcess.Created;

        //assert
        Assert.Equal(exptectedStatus, actual.Status);
    }

    private Author GetAuthor()
    {
        return new Author
        {
            Id = 10,
            Cpf = "07685817101",
            CreatedTime = DateTime.Now,
            Depoiment = "my girl creat me",
            Name = "Aatrox Sad",
            PersonId = 10,
            UpadatedTime = null,
            Person = new Person
            {
                Cpf = "07685817101",
                FirstName = "Aatrox",
                LastName = "Sad",
                BirthDate = DateTime.Now.AddYears(-25),
                CreatedTime = DateTime.Now,
                Id = 10,
                UpadatedTime = null
            }
        };
    }

    private Accused GetAccused()
    {
        return new Accused
        {
            Id = 15,
            Cpf = "07685817105",
            CreatedTime = DateTime.Now,
            Name = "Aatrox happy",
            PersonId = 10,
            UpadatedTime = null,
            Person = new Person
            {
                Cpf = "07685817105",
                FirstName = "Aatrox",
                LastName = "happy",
                BirthDate = DateTime.Now.AddYears(-30),
                CreatedTime = DateTime.Now,
                Id = 15,
                UpadatedTime = null
            }
        };
    }

    private Person GetPerson()
    {
        return new Person
        {
            Id = 5,
            BirthDate = DateTime.Now.AddYears(-25),
            Cpf = "07685817101",
            CreatedTime = DateTime.Now,
            FirstName = "Amumu",
            LastName = "Lulu",
            UpadatedTime = null
        };
    }

    private JudicialProcessCreateRequest GetRequest()
    {
        return new JudicialProcessCreateRequest
        {
            AccusedId = 5,
            AuthorDepoiment = "my girl cheat me",
            AuthorId = 3,
            LawyerId = 1,
            Reason = "cheat",
            RequestedValue = 1988.33M
        };
    }

    private Lawyer GetLawyer()
    {
        return new Lawyer
        {
            CreatedTime = DateTime.Now,
            Id = 1,
            Name = "Caitlyn",
            Oab = "06665333",
            Person = GetPerson(),
            PersonId = 5,
            UpadatedTime = null
        };
    }


}
