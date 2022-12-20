using Microsoft.AspNetCore.Mvc.Testing;

namespace Delega.Tests.IntegrationTests;

public class BaseTests
{
    public readonly WebApplicationFactory<Program> _factory;

    public BaseTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
}
