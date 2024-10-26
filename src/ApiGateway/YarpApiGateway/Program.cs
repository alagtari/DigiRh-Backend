using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using YarpApiGateway.Middlewares;
using YarpApiGateway.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

builder.Services.AddHttpClient();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

var app = builder.Build();


app.UseExceptionHandler("/error");

app.UseCors("AllowAnyOrigin");

app.UseRateLimiter();

app.UseMiddleware<JwtMiddleware>();

app.MapReverseProxy();

app.Run();