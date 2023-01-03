using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Xunit;

namespace Delega.Tests.IntegrationTests;

public class AddressControllerIntegrationTest : IntegrationTestBase
{
    private const string baseUrl = $@"https://localhost:7179/api/address";

    public AddressControllerIntegrationTest(WebApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task GET_ShouldReturn200OKAddress()
    {
        /* busca um objeto cujo id = 1 
         * verifica se o obj não é nulo 
         * verifica se o a controller retornou um 200OK */

        var id = 1;

        var response = await _client.GetAsync($"{baseUrl}/{id}");
        var objJson = await response.Content.ReadAsStringAsync();
        var address = JsonConvert.DeserializeObject<AddressResponse>(objJson);

        Convert.ToInt32(response.StatusCode).Should().Be(200);
        address.Should().NotBeNull();
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
         * verifica se controller retornou um 201 Created 
         * verifica se o disctric é igual o gerado aleatorio */

        var district = TestUtils.RandomString(1);
        var street = TestUtils.RandomString(3);
        var zipCode = TestUtils.RandomString(3);

        var insertAddress = new AddressCreateDTO
        {
            CityId = 1,
            District = district,
            Street = street,
            ZipCode = zipCode
        };

        var addressJson = JsonContent.Create(insertAddress);
        var response = await _client.PostAsync($"{baseUrl}", addressJson);
        var responseJson = await response.Content.ReadAsStringAsync();
        var addressResponse = JsonConvert.DeserializeObject<AddressResponse>(responseJson);

        Convert.ToInt32(response.StatusCode).Should().Be(201);
        addressResponse.Should().NotBeNull();
        addressResponse?.District.Should().Be(district);
        addressResponse?.Street.Should().Be(street);
        addressResponse?.ZipCode.Should().Be(zipCode);
    }
    
    [Theory(DisplayName = "Should return 400 invalid disctrict")]
    [InlineData("")]
    [InlineData(null)]
    public async Task POST_ShouldReturn400BadRequestInvalidDistrict(string district)
    {
        /* Cria um DTO com a propriedade district inválida
         * verifica se o retorno é 400 bad request
         * verifica se a resposta de erro contém a mensagem "invalid distritct" */

        var street = TestUtils.RandomString(3);
        var zipCode = TestUtils.RandomString(3);

        var insertAddress = new AddressCreateDTO
        {
            CityId = 1,
            District = district,
            Street = street,
            ZipCode = zipCode
        };

        var addressJson = JsonContent.Create(insertAddress);
        var response = await _client.PostAsync(baseUrl, addressJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("district", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }

    [Theory(DisplayName = "Should return 400 invalid street")]
    [InlineData("x")]
    [InlineData("xx")]
    [InlineData(null)]
    public async Task POST_ShouldReturn400BadRequestInvalidStreet(string street)
    {
        /* Cria um DTO com a propriedade district inválida
         * verifica se o retorno é 400 bad request
         * verifica se a resposta de erro contém a mensagem "invalid distritct" */

        var zipCode = TestUtils.RandomString(3);
        var district = TestUtils.RandomString(3);

        var insertAddress = new AddressCreateDTO
        {
            CityId = 1,
            District = district,
            Street = street,
            ZipCode = zipCode
        };

        var addressJson = JsonContent.Create(insertAddress);
        var response = await _client.PostAsync(baseUrl, addressJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("street", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }

    [Theory(DisplayName = "Should return 400 invalid ZipCode")]
    [InlineData("x")]
    [InlineData("xx")]
    [InlineData(null)]
    public async Task POST_ShouldReturn400BadRequestInvalidZipCode(string zipCode)
    {
        /* Cria um DTO com a propriedade district inválida
         * verifica se o retorno é 400 bad request
         * verifica se a resposta de erro contém a mensagem "invalid distritct" */

        var street = TestUtils.RandomString(3);
        var district = TestUtils.RandomString(3);

        var insertAddress = new AddressCreateDTO
        {
            CityId = 1,
            District = district,
            Street = street,
            ZipCode = zipCode
        };

        var addressJson = JsonContent.Create(insertAddress);
        var response = await _client.PostAsync(baseUrl, addressJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("zip code", StringComparison.InvariantCultureIgnoreCase) || responseString.Contains("zipcode", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }

    [Theory(DisplayName = "Should return 400 invalid city id")]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task POST_ShouldReturn400BadRequestInvalidCityId(long cityId)
    {
        /* Cria um DTO com a propriedade cityId inválida
         * verifica se o retorno é 400 bad request
         * verifica se a resposta de erro contém a mensagem "invalid distritct" */

        var street = TestUtils.RandomString(3);
        var district = TestUtils.RandomString(3);
        var zipCode = TestUtils.RandomString(3);

        var insertAddress = new AddressCreateDTO
        {
            CityId = cityId,
            District = district,
            Street = street,
            ZipCode = zipCode
        };

        var addressJson = JsonContent.Create(insertAddress);
        var response = await _client.PostAsync(baseUrl, addressJson);
        var responseString = await response.Content.ReadAsStringAsync();

        var contains = responseString.Contains("invalid city id", StringComparison.InvariantCultureIgnoreCase);
        Convert.ToInt32(response.StatusCode).Should().Be(400);
        contains.Should().Be(true);
    }
}