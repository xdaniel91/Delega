using Delega.Infraestrutura.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly IConfigurationRoot Configuration;

    public CustomWebApplicationFactory(IConfigurationRoot configuration) 
    {
        Configuration = configuration;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<DelegaContext>));

            services.Remove(descriptor);

            services.AddDbContext<DelegaContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("delega.postgres"));
            });
        });

        builder.UseEnvironment("Development");
    }
}