using Delega.Api.Database;
using Delega.Api.Interfaces;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Services.Implementation;
using Moq;
using Xunit;

namespace Delega.Testes
{
    public class PersonServiceTest
    {
        private PersonService personService;

        public PersonServiceTest()
        {
            this.personService = new PersonService(
                new Mock<IPersonRepository>().Object,
                new Mock<IUnitOfWork>().Object,
                new Mock<IConsMessages>().Object);
        }

        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}