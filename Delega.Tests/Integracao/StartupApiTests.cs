using Delega.Infraestrutura.Repositories_Implementation;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Implementation;
using Delega.Infraestrutura.Services_Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delega.Tests.Integracao;

public class StartupApiTests
{
    public StartupApiTests(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //services
        services.AddTransient<IAddressService, AddressService>();
        services.AddTransient<ICityService, CityService>();
        services.AddTransient<ICountryService, CountryService>();
        services.AddTransient<IPersonService, PersonService>();
        services.AddTransient<IStateService, StateService>();

        //repositories
        services.AddTransient<IAddressRepository, AddressRepository>();
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<ICountryRepository, CountryRepository>();
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IStateRepository, StateRepository>();

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseMvc();
    }
}
