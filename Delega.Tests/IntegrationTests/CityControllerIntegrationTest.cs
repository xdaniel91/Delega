using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Xunit;

namespace Delega.Tests.IntegrationTests;

public class CityControllerIntegrationTest : IntegrationTestBase
{
    private const string baseUrl = $@"https://localhost:7179/api/city";
    public CityControllerIntegrationTest(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task GET_ShouldReturn200OKCity()
    {
        /* busca um objeto cujo id = 1 
         * verifica se o obj não é nulo 
         * verifica se o a controller retornou um 200OK */

        var id = 1;

        var response = await _client.GetAsync($"{baseUrl}/{id}");
        var cityJson = await response.Content.ReadAsStringAsync();
        var city = JsonConvert.DeserializeObject<CityResponse>(cityJson);

        Convert.ToInt32(response.StatusCode).Should().Be(200);
        city.Should().NotBeNull();
    }

    [Fact]
    public async Task GET_ShouldReturn404NotFound()
    {
        /* Busca um id que não existe no banco
         * verifica se a controller retornou um 404 not found */

        var id = 199999;

        var response = await _client.GetAsync($"{baseUrl}/{id}");

        Convert.ToInt32(response.StatusCode).Should().Be(404);
    }

    [Fact]
    public async Task POST_ShouldReturn201Created()
    {
        /* Cria um DTO e faz uma request para inserir o novo objeto
         * verifica se o objeto retornado não é nulo
         * verifica se controller retornou um 201 Created */

        var name = TestUtils.RandomString(10);

        var insertCity = new CityCreateDTO
        {
            Name = name,
            StateId = 1
        };

        var cityJson = JsonContent.Create(insertCity);
        var response = await _client.PostAsync($"{baseUrl}", cityJson);
        var responseJson = await response.Content.ReadAsStringAsync();
        var cityResponse = JsonConvert.DeserializeObject<CityResponse>(responseJson);

        Convert.ToInt32(response.StatusCode).Should().Be(201);
        cityResponse.Should().NotBeNull();
        cityResponse?.Name.Should().Be(name);
    }

    [Theory(DisplayName = "Should return 400 invalid name")]
    [InlineData("")]
    [InlineData(null)]
    public async Task POST_ShouldReturn400BadRequestInvalidName(string input)
    {
        /* Atribui uma string vazia e null ao nome do objeto a ser inserido 
         * verifica se o erro retornado contém a palavra "name"
         * verifica se se a controller retornou um 400 bad request */

        var insertCity = new CityCreateDTO
        {
            Name = input,
            StateId = 1
        };
        var cityJson = JsonContent.Create(insertCity);
        var response = await _client.PostAsync(baseUrl, cityJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("name", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }

    [Theory(DisplayName = "Should return 400 invalid stateId")]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task POST_ShouldReturn400BadRequestInvalidStateId(long input)
    {
        /* Atribui valores inválidos ao id do estado
         * o obj não deve ser criado
         * verificado se http resposta contém a palavra "state"
         * verificado se o retorno da request é um 400 bad request */

        var randomName = TestUtils.RandomString(5);

        var cityCreate = new CityCreateDTO
        {
            Name = randomName,
            StateId = input
        };

        var cityJson = JsonContent.Create(cityCreate);
        var response = await _client.PostAsync(baseUrl, cityJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("state", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }
}
