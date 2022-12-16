using Delega.Infraestrutura.Repositories_Implementation;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Implementation;
using Delega.Infraestrutura.Services_Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Delega.Tests.Integracao;

public class IntegrationTestFixture
{
    public IntegrationTestFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IAddressService, AddressService>();
        serviceCollection.AddTransient<ICityService, CityService>();
        serviceCollection.AddTransient<ICountryService, CountryService>();
        serviceCollection.AddTransient<IPersonService, PersonService>();
        serviceCollection.AddTransient<IStateService, StateService>();
        serviceCollection.AddTransient<IAddressRepository, AddressRepository>();
        serviceCollection.AddTransient<ICityRepository, CityRepository>();
        serviceCollection.AddTransient<ICountryRepository, CountryRepository>();
        serviceCollection.AddTransient<IPersonRepository, PersonRepository>();
        serviceCollection.AddTransient<IStateRepository, StateRepository>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
    public ServiceProvider ServiceProvider { get; private set; }
}
