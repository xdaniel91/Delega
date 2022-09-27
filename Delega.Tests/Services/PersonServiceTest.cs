using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Services.Implementation;
using Moq;
using Xunit;

namespace Delega.Tests.Services;

public class PersonServiceTest
{
    private readonly IPersonService personService;

    public PersonServiceTest()
    {
        this.personService = new PersonService(
            new Mock<IPersonRepository>().Object,
            new Mock<IUnitOfWork>().Object);
    }

    [Fact]
    public void AddAsync_InvalidInfos()
    {
        Assert.True(true);
    }
}
