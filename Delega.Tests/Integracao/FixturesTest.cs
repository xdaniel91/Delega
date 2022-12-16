using System.Net.Http.Json;
using Xunit;

namespace Delega.Tests.Integracao;

[Collection(nameof(IntegrationApiTestFixtureCollection))]
public class FixturesTest
{
    private readonly IntegrationTestFixture<StartupApiTests> _integrationTestFixture;
    public FixturesTest(IntegrationTestFixture<StartupApiTests> integrationTestFixture)
    {
        _integrationTestFixture = integrationTestFixture;
    }

    [Theory(DisplayName = "Insert country via API")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetAddressByAPI(int countryName)
    {
        var response = await _integrationTestFixture.Client.GetAsync($@"https://localhost:7179/api/address/1");
        var resposta = await response.Content.ReadAsStringAsync();

        Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }
}