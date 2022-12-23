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

public class StateControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private const string baseUrl = $@"https://localhost:7179/api/state";

    public StateControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GET_ShouldReturn200OK()
    {
        var id = 1;
        var response = await _client.GetAsync($"{baseUrl}/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var state = JsonConvert.DeserializeObject<StateResponse>(content);

        Convert.ToInt32(response.StatusCode).Should().Be(200);
        state.Should().NotBeNull();
    }

    [Fact]
    public async Task GET_ShouldReturn404NotFound()
    {
        var id = 19999;
        var response = await _client.GetAsync($"{baseUrl}/{id}");
        var content = await response.Content.ReadAsStringAsync();

        Convert.ToInt32(response.StatusCode).Should().Be(404);
        content.Contains("not found", StringComparison.InvariantCultureIgnoreCase).Should().BeTrue();
    }

    [Fact]
    public async Task Post_ShouldReturn201Created()
    {
        var name = TestUtils.RandomString(5);

        var stateInsert = new StateCreateDTO
        {
            CountryId = 1,
            Name = name
        };

        var stateInsertJson = JsonContent.Create(stateInsert);
        var response = await _client.PostAsync($"{baseUrl}", stateInsertJson);
        var content = await response.Content.ReadAsStringAsync();
        var stateInserted = JsonConvert.DeserializeObject<StateResponse>(content);

        Convert.ToInt32(response.StatusCode).Should().Be(201);
        stateInserted.Should().NotBeNull();
        stateInserted?.Name.Should().Be(name);
    }


    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("xd")]
    public async Task Post_ShoudReturn400InvalidFields(string name)
    {
        var stateInsert = new StateCreateDTO
        {
            CountryId = 1,
            Name = name
        };

        var stateInsertJson = JsonContent.Create(stateInsert);
        var response = await _client.PostAsync(baseUrl, stateInsertJson);
        var content = await response.Content.ReadAsStringAsync();

        var xx = "";

        Convert.ToInt32(response.StatusCode).Should().Be(400);
        content.Contains("name", StringComparison.InvariantCultureIgnoreCase).Should().BeTrue();
    }

    [Fact]
    public async Task Patch_ShouldReturn200OK()
    {
        var name = TestUtils.RandomString(5);

        var stateUpdate = new StateUpdateDTO
        {
            CountryId = 1,
            Id = 1,
            Name = name
        };

        var stateUpdateJson = JsonContent.Create(stateUpdate);
        var response = await _client.PatchAsync(baseUrl, stateUpdateJson);
        var content = await response.Content.ReadAsStringAsync();
        var stateUpdated = JsonConvert.DeserializeObject<StateResponse>(content);

        Convert.ToInt32(response.StatusCode).Should().Be(200);
        stateUpdated.Should().NotBeNull();
        stateUpdated?.Name.Should().Be(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Patch_ShoudReturn200OEmptyFields(string name)
    {
        /* recebe valores vazios, e não altera nenhum valor do obj original */ 

        var stateUpdate = new StateUpdateDTO
        {
            CountryId = 1,
            Id = 1,
            Name = name
        };

        var stateUpdateJson = JsonContent.Create(stateUpdate);
        var response = await _client.PatchAsync(baseUrl, stateUpdateJson);
        var content = await response.Content.ReadAsStringAsync();
        var stateResponse = JsonConvert.DeserializeObject<StateResponse>(content);

        Convert.ToInt32(response.StatusCode).Should().Be(200);
        stateResponse?.Name.Should().NotBe(stateUpdate.Name);
    }

    [Fact]
    public async Task Patch_ShouldReturn400BadRequestInvalidName()
    {
        var stateUpdate = new StateUpdateDTO
        {
            CountryId = 1,
            Id = 1,
            Name = "xd"
        };

        var stateUpdateJson = JsonContent.Create(stateUpdate);
        var response = await _client.PatchAsync(baseUrl, stateUpdateJson);
        var content = await response.Content.ReadAsStringAsync();

        Convert.ToInt32(response.StatusCode).Should().Be(400);
        content.Contains("invalid").Should().BeTrue();
    }
}
