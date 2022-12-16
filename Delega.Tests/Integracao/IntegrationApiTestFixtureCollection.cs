using Xunit;


namespace Delega.Tests.Integracao;

[CollectionDefinition(nameof(IntegrationApiTestFixtureCollection))]
public class IntegrationApiTestFixtureCollection : ICollectionFixture<IntegrationTestFixture<StartupApiTests>>
{

}
public class IntegrationTestFixture<TStartup> : IDisposable where TStartup : class
{
    public readonly DelegaAppFactory<TStartup> Factory;
    public HttpClient Client;

    public IntegrationTestFixture()
    {
        var clientOptions = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
        {
            HandleCookies = false,
            BaseAddress = new Uri("http://localhost"),
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 7
        };

        Factory = new DelegaAppFactory<TStartup>();
        Client = Factory.CreateClient(clientOptions);
    }
    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}
