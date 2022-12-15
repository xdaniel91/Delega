using Delega.Infraestrutura.Database;
using Delega.IoC;

namespace Delega.Api.Configurations;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        //// Context
        services.AddScoped<DelegaContext>();

        //entities
        DelegaEntities.AddDelegaEntities(services);
    }
}
