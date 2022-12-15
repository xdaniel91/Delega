using Delega.Application.Repositories_Interfaces;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Repositories_Implementation;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Implementation;
using Delega.Infraestrutura.Services_Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delega.IoC;

public static class DelegaEntities
{
    public static void AddContext(IServiceCollection services, string connectionString)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<DelegaContext>(
            context => {
                context.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly("Delega.Infraestrutura"));
            });
    }

    public static void AddDelegaEntities(this IServiceCollection services)
    {
        services.AddScoped<IUow, Uow>();

        //services
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IStateService, StateService>();
        
        //repositories
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IStateRepository, StateRepository>();
    }
}
