using Delega.Infraestrutura.Database;
using Microsoft.EntityFrameworkCore;
using Delega.Api.Configurations;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencyInjection();
builder.Services.AddFluentMigrator(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddDbContext<DelegaContext>(
options => options.UseNpgsql(builder.Configuration.GetConnectionString("delega.postgres")));

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
app.UpdateDatabase();
app.Run();

public partial class Program { }