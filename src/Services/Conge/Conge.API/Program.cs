using Conge.Infrastructure.Data.Extensions;
using Conge.API;
using Conge.Application;
using Conge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
var app = builder.Build();





await app.InitialiseDatabaseAsync();

app.UseApiServices();
app.Run();