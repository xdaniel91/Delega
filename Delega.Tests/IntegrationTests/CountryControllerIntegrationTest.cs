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

public class CountryControllerIntegrationTest : IntegrationTestBase
{
    private const string baseUrl = $@"https://localhost:7179/api/country";

    public CountryControllerIntegrationTest(WebApplicationFactory<Program> factory) : base(factory) { }


    [Fact]
    public async Task GET_ShouldReturn200OKCountry()
    {
        /* busca um objeto cujo id = 1 
         * verifica se o obj não é nulo 
         * verifica se o a controller retornou um 200OK */

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
        /* Busca um id que não existe no banco
         * verifica se a controller retornou um 404 not found */

        var id = 199999;

        var response = await _client.GetAsync($"{baseUrl}/{id}");

        Convert.ToInt32(response.StatusCode).Should().Be(404);
    }

    [Fact]
    public async Task POST_ShouldReturnInsertedCountry()
    {
        /* Cria um DTO e faz uma requisição para inserir o novo objeto
         * verifica se o objeto retornado não é nulo
         * verifica se controller retornou um 201 Created */

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
        /* Atribui uma string vazia e null ao nome do objeto a ser inserido 
         * verifica se o erro retornado contém a palavra "name"
         * verifica se se a controller retornou um 400 bad request */

        var insertCountry = new CountryCreateDTO
        {
            Name = name
        };
        var countryJson = JsonContent.Create(insertCountry);
        var response = await _client.PostAsync(baseUrl, countryJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("name", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }

    [Fact]
    public async Task Patch_ShouldReturn200OK()
    {
        /* Cria um nome aleatório de 5 letras - e altera o objeto de id = 1 
         * verifica se a controller retornou 200OK 
         * verifica se o nome aleatório que foi gerado é igual ao que foi retornado */

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
        /* é passado uma string vazia e null para o parâmetro name
         * ambos deverão ser ignorados e não ocorrerá nenhuma atualização (pois o método é um patch)
         * verifica se o nome do obj no banco é diferente do parâmetro name */

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
