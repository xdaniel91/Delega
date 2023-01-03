using Delega.Dominio.Entities;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Xunit;

namespace Delega.Tests.IntegrationTests;

public class PersonControllerIntegragationTest : IntegrationTestBase
{
    private const string baseUrl = @"https://localhost:7179/api/person";

    public PersonControllerIntegragationTest(WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task GET_ShouldRetun200OK()
    {
        var id = 1;

        //Act
        var response = await _client.GetAsync($"{baseUrl}/{id}");

        //Act
        var person = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());

        //Assert
        Assert.NotNull(person);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GET_ShouldReturn404NotFound()
    {

        //Act
        var response = await _client.GetAsync($"{baseUrl}/199999999999");

        //Assert
        Assert.Equal(404, (int)response.StatusCode);
    }

    [Fact]
    public async Task POST_ShouldReturn201Created()
    {
        var cpf = TestUtils.RandomString(11);
        var firstName = TestUtils.RandomString(5);
        var lastName = TestUtils.RandomString(5);
       
        //Arrange
        var personInsert = new PersonCreateDTO
        {
            AddressId = 1,
            BirthDate = DateTime.Today.AddYears(-35),
            Cpf = cpf,
            FirstName = firstName,
            LastName = lastName
        };

        //Act
        var personJson = JsonContent.Create(personInsert);
        var response = await _client.PostAsync(baseUrl, personJson);
        Convert.ToInt32(response.StatusCode).Should().Be(201);
    }

    [Theory]
    [InlineData("Daniel", "Rodrigues", "07685817100", "2045-02-05")]
    [InlineData("Dn", "Rod", "12345678910", "1999-02-05")]
    [InlineData(null, "Rodrigues", "07685817100", "1999-02-05")]
    [InlineData("", "Rodrigues", "07685817100", "1999-02-05")]
    [InlineData("Daniel", "", "07685817100", "1999-02-05")]
    [InlineData("Daniel", null, "07685817100", "1999-02-05")]
    [InlineData("Daniel", "Rodrigues", "", "1999-02-05")]
    [InlineData("Daniel", "Rodrigues", null, "2022-02-05")]
    [InlineData("Daniel", "Rodrigues", "12345678911", null)]
    [InlineData("Daniel", "Rodrigues", "1234567891155", "1999-02-05")]
    public async Task POST_ShouldRetun400BadRequestInvalidPersonInfos(string firstname, string lastname, string cpf, string birthdate)
    {
        /* tenta adicionar uma pessoa com dados inválidos */

        //Arrange
        var personInsert = new PersonCreateDTO
        {
            AddressId = 1,
            BirthDate = Convert.ToDateTime(birthdate),
            Cpf = cpf,
            FirstName = firstname,
            LastName = lastname
        };
        var personJson = JsonContent.Create(personInsert);

        //Act
        var response = await _client.PostAsync(baseUrl, personJson);

        //Assert
        Assert.Equal(400, (int)response.StatusCode);
    }

    [Fact]
    public async Task PATCH_ShoudReturn200OK()
    {
        //Arrange 
        var personUpdate = new PersonUpdateDTO
        {
            BirthDate = DateTime.Today.AddYears(-28),
            Cpf = "16666987856",
            FirstName = "Celulo",
            LastName = "Mario",
            Id = 6
        };
        var personJson = JsonContent.Create(personUpdate);

        //Act
        var response = await _client.PatchAsync(baseUrl, personJson);
        var personString = await response.Content.ReadAsStringAsync();
        var personUpdated = JsonConvert.DeserializeObject<PersonResponse>(personString);

        //Assert
        Assert.Equal(personUpdate.Cpf, personUpdated?.Cpf);
    }

    [Theory]
    [InlineData("Da", "Rodrigues", "12345678911", "1999-01-01")]
    [InlineData("Dan", "Ro", "11155599977", "1999-02-02")]
    [InlineData(null, null, "123456", "1999-03-03")]
    [InlineData(null, null, null, "2038-04-04")]
    [InlineData(null, null, null, null)]
    public async Task PATCH_ShouldReturn400BadRequestInvalidFields(string firstname, string lastname, string cpf, string birth)
    {
        //Arrange
        var personUpdate = new PersonUpdateDTO
        {
            BirthDate = Convert.ToDateTime(birth),
            Cpf = cpf,
            FirstName = firstname,
            Id = 5,
            LastName = lastname
        };
        var personJson = JsonContent.Create(personUpdate);

        //Act
        var response = await _client.PatchAsync(baseUrl, personJson);
        var xx = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(400, (int)response.StatusCode);
    }
}