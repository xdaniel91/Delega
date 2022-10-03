using Delega.Api.Database;
using Delega.Api.Models;
using Delega.Api.Repositories.Implementation;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Delega.Tests.Repositories
{
    public class PersonRepositoryTest
    {
        private PersonRepository _personRepository;

        public PersonRepositoryTest()
        {
            _personRepository = new PersonRepository(new Mock<DelegaContext>().Object);
        }

        [Fact(DisplayName = "Add returns person")]
        public async Task Add_ReturnsPersonAsync()
        {
           
            var mockContext = new Mock<DelegaContext>();
            IList<Person> data = new List<Person>
            {
                new Person
                {
                    BirthDate = DateTime.Now.AddYears(-30),
                    Cpf = "07685817101",
                    CreatedTime = DateTime.Now,
                    FirstName = "Kai'sa",
                    LastName = "Kassadin",
                    Id = 1,
                    UpadatedTime = null
                 },

                new Person
                {
                    BirthDate = DateTime.Now.AddYears(-21),
                    Cpf = "07685817101",
                    CreatedTime = DateTime.Now,
                    FirstName = "Nasus",
                    LastName = "Garen",
                    Id = 2,
                    UpadatedTime = null
                },

                new Person
                {
                    BirthDate = DateTime.Now.AddYears(-26),
                    Cpf = "07685817101",
                    CreatedTime = DateTime.Now,
                    FirstName = "Darius",
                    LastName = "Ashe",
                    Id = 3,
                    UpadatedTime = null
                },

            };
            mockContext.Setup(x => x.person).ReturnsDbSet(data);

            _personRepository = new PersonRepository(mockContext.Object);

            var _personInsert = new Person
            {
                BirthDate = DateTime.Now.AddYears(-28),
                Cpf = "07685817101",
                CreatedTime = DateTime.Now,
                FirstName = "Tryndamare",
                LastName = "Brand",
                Id = 5,
                UpadatedTime = null
            };

            var result = await _personRepository.AddAsync(_personInsert, CancellationToken.None);

            Assert.NotNull(result);
        }

    }
}
