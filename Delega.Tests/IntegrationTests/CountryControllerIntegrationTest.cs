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

public class CountryControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private const string baseUrl = $@"https://localhost:7179/api/country";

    public CountryControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GET_ShouldReturn200OKCountry()
    {
        var id = 1;

        var response = await _client.GetAsync($"{baseUrl}/{id}");
        var countryJson = await response.Content.ReadAsStringAsync();
        var country = JsonConvert.DeserializeObject<CountryResponse>(countryJson);
       
        Convert.ToInt32(response.StatusCode).Should().Be(200);
        country.Should().NotBeNull();
    }

    [Fact]
    public async Task GET_SholdReturn404NotFoundCountry()
    {
        var id = 199999;

        var response = await _client.GetAsync($"{baseUrl}/{id}");

        Convert.ToInt32(response.StatusCode).Should().Be(400);
    }

    [Fact]
    public async Task POST_ShouldReturnInsertedCountry()
    {
        var name = TestUtils.RandomString(10);

        var insertCountry = new CountryCreateDTO
        {
            Name = name
        };

        var countryJson = JsonContent.Create(insertCountry);
        var response = await _client.PostAsync($"{baseUrl}", countryJson);
        var responseJson = await response.Content.ReadAsStringAsync();
        var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(responseJson);

        Convert.ToInt32(response.StatusCode).Should().Be(201);
        countryResponse.Should().NotBeNull();
        countryResponse?.Name.Should().Be(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task POST_ShouldReturn400BadRequestInvalidFields(string? name)
    {
        var insertCountry = new CountryCreateDTO
        {
            Name = name
        };

        var countryJson = JsonContent.Create(insertCountry);
        var response = await _client.PostAsync(baseUrl, countryJson);
        var responseString = await response.Content.ReadAsStringAsync();
        var xx = "";

        Convert.ToInt32(response.StatusCode).Should().Be(400);
        var contains = responseString.Contains("name", StringComparison.InvariantCultureIgnoreCase); 
        contains.Should().Be(true);
    }

    [Fact]
    public async Task Patch_ShouldReturn200OK()
    {
        var name = TestUtils.RandomString(5);

        var updateObj = new CountryUpdateDTO
        {
            Id = 1,
            Name = name
        };

        var objJson = JsonContent.Create(updateObj);
        var resposta = await _client.PatchAsync(baseUrl, objJson);
        var content = await resposta.Content.ReadAsStringAsync();
        var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(content);

        Convert.ToInt32(resposta.StatusCode).Should().Be(200);
        countryResponse.Should().NotBeNull();
        countryResponse?.Name.Should().Be(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Patch_ShouldReturn200OKInvalidFields(string name)
    {
        /* dados inválidos que serão ignorados e o */

        var updatedObj = new CountryUpdateDTO
        {
            Id = 1,
            Name = name
        };

        var objJson = JsonContent.Create(updatedObj);
        var response = await _client.PatchAsync(baseUrl, objJson);
        var content = await response.Content.ReadAsStringAsync();
        var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(content);

        Convert.ToInt32(response.StatusCode).Should().Be(200);
        countryResponse?.Name.Should().NotBe(updatedObj.Name);
    }


}
