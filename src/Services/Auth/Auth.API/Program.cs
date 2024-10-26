using System.Reflection;
using Auth.API.Data.Extensions;
using Auth.API.Features.Login;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddScoped<TokenValidator>();
builder.Services.AddCarter();
builder.Services.AddDbContext<ApplicationDbContext>((options) => { options.UseSqlServer(connectionString); });
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddSingleton<TokenGenerator>();

var app = builder.Build();


await app.InitialiseDatabaseAsync();


app.MapCarter();
app.UseExceptionHandler(options => { });


app.Run();