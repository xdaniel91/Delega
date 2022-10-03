using Delega.Api.Database;
using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Services.Implementation;
using Delega.Api.Services.Interfaces;
using Moq;
using Xunit;

namespace Delega.Tests.Services;

public class LawyerServiceTest
{
    private ILawyerService _service;

    public LawyerServiceTest()
    {
        _service = new LawyerService(
            new Mock<ILawyerRepository>().Object,
            new Mock<IPersonRepository>().Object,
            new Mock<IUnitOfWork>().Object);
    }

    [Theory(DisplayName = "AddAsync fail")]
    [InlineData("65968263814", 0)]
    [InlineData("", 5)]
    public async Task AddAsync_Fail(string oab, int pessoaId)
    {
        var request = new LawyerCreateRequest
        {
            Oab = oab,
            PersonId = pessoaId
        };

        var ex = await Assert.ThrowsAsync<DelegaException>(async () => await _service.AddAsync(request));

        var containsResult = (ex.Message.Contains("Invalid person id.") || ex.Message.Contains("Person not found."));
        Assert.True(containsResult);

    }

    [Fact(DisplayName = "AddAsync sucess")]
    public async Task AddAsync_Sucess()
    {
        var id = 5;
        var oab = "65968263814";

        var request = new LawyerCreateRequest
        {
            Oab = oab,
            PersonId = id
        };

        var _person = new Person
        {
            BirthDate = DateTime.Now.AddYears(-25),
            Cpf = "07685817101",
            CreatedTime = DateTime.Now,
            FirstName = "Kassadin",
            LastName = "Vlad",
            Id = 1,
            UpadatedTime = null
        };
        
        var _lawyerResponse = new LawyerResponse
        {
            Id = id,
            Oab = request.Oab,
            PersonFirstName = "kassadin",
            PersonLastName = "do vazio",
            PersonId = 1
        };

        var _personRepositoryMock = new Mock<IPersonRepository>();
        var _lawyerRepositoryMock = new Mock<ILawyerRepository>();

        _personRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(_person);
        _lawyerRepositoryMock.Setup(x => x.GetResponse(id)).Returns(_lawyerResponse);
      
        _service = new LawyerService(_lawyerRepositoryMock.Object, _personRepositoryMock.Object, new Mock<IUnitOfWork>().Object);

        var response = await _service.AddAsync(request);

        Assert.Null(response);
    }

    [Fact(DisplayName = "Get response sucess")]
    public void GetResponse_Sucess()
    {
        var response = new LawyerResponse
        {
            Id = 5,
            Oab = "72899950776",
            PersonFirstName = "Soraka",
            PersonLastName = "Nami",
            PersonId = 10
        };

        var _lawyerRepositoryMock = new Mock<ILawyerRepository>();
        _lawyerRepositoryMock.Setup(x => x.GetResponse(response.Id)).Returns(response);
        
        _service = new LawyerService(_lawyerRepositoryMock.Object, new Mock<IPersonRepository>().Object, new Mock<IUnitOfWork>().Object);

        var result = _service.GetResponse(5);

        Assert.Equal(response.Oab, result.Oab);
    }
}
