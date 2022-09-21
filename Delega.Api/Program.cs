using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Interfaces.Services;
using Delega.Api.Repositories.Implementation;
using Delega.Api.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner;
using Delega.Api.Migrations;
using Delega.Api.Utils;
using Delega.Api.Interfaces;

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

builder.Services.AddScoped<IConsMessages, ConstMessages>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddScoped<IJudicialProcessService, JudicialProcessService>();
builder.Services.AddScoped<IJudicialProcessRepository, JudicialProcessRepository>();

builder.Services.AddScoped<ILawyerService, LawyerService>();
builder.Services.AddScoped<ILawyerRepository, LawyerRepository>();
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
    .ScanIn(typeof(AddPersonTable).Assembly).For.Migrations()
    .ScanIn(typeof(AddLawyerTable).Assembly).For.Migrations()
    .ScanIn(typeof(AddAuthorTable).Assembly).For.Migrations()
    .ScanIn(typeof(AddAccusedTable).Assembly).For.Migrations()
    )
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);
}
