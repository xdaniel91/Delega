using Delega.Infraestrutura.Migrations;
using FluentMigrator.Runner;

namespace Delega.Api.Configurations;

public static class FluentMigrator
{
    public static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
             .ConfigureRunner(c => c.AddPostgres()
        .WithGlobalConnectionString(configuration.GetConnectionString("delega.postgres"))
        .ScanIn(typeof(CountryMigration).Assembly).For.Migrations())

         .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);
    }

    public static void UpdateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        runner.MigrateUp();
    }
}
