using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Entities;
using Delega.Dominio.Exceptions;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Implementation;
using Delega.Infraestrutura.Services_Interfaces;
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
            new Mock<IUow>().Object);
    }

    [Theory]
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
    public async Task CreatePerson_ThrowsExceptionInvalidFields(string firstname, string lastname, string cpf, string birth)
    {
        //ARRANGE
        var personCad = new PersonCreateDTO
        {
            FirstName = firstname,
            BirthDate = Convert.ToDateTime(birth),
            Cpf = cpf,
            LastName = lastname
        };
        var cancellationToken = new CancellationToken();

        //ACT
        var ex = await Assert.ThrowsAsync<DelegaDomainException>(async () => await
        personService.AddPersonAsync(personCad, cancellationToken));

        //ASSERT
        Assert.True(ex is DelegaDomainException);
    }


    [Fact]
    public async void CreatePersonSucess()
    {
        //ARRANGE
        var personCad = new PersonCreateDTO
        {
            FirstName = "Khwyenno",
            LastName = "Lehokker",
            BirthDate = DateTime.Today.AddYears(-28),
            Cpf = "07685817101",
        };
        var cancellationToken = new CancellationToken();

        //ACT
        var insertedPerson = await personService.AddPersonAsync(personCad, cancellationToken);

        var isEqual =
            personCad.FirstName.Equals(insertedPerson.FirstName)
            && personCad.LastName.Equals(insertedPerson.LastName)
            && personCad.BirthDate.Equals(insertedPerson.BirthDate);

        //ASSERT
        Assert.True(isEqual);
    }
}
