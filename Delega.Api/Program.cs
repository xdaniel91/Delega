using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Interfaces.Repositories;
using Delega.Infraestrutura.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner;
using Delega.Infraestrutura.Migrations;
using Delega.Infraestrutura.Database;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var ServiceProvider = CreateServices(builder);

builder.Services.AddControllers();
builder.Services.AddDbContext<DelegaContext>(
options => options.UseNpgsql(builder.Configuration.GetConnectionString("delega.postgres")));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("delega.redis");
});

using var scope = ServiceProvider.CreateScope();
UpdateDatabase(scope.ServiceProvider);

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWok>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void UpdateDatabase(IServiceProvider serviceProvider)
{
    // Instantiate the runner
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    // Execute the migrations
    runner.MigrateUp();
}

static IServiceProvider CreateServices(WebApplicationBuilder builder)
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddPostgres()
    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("delega.postgres"))
    .ScanIn(typeof(PersonMigration).Assembly).For.Migrations()
    .ScanIn(typeof(FeedPersonTable).Assembly).For.Migrations()
    )
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);
}
