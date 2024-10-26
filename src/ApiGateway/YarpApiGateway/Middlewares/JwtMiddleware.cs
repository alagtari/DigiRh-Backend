using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using YarpApiGateway.Settings;

namespace YarpApiGateway.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<AuthSettings> _authSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(
        RequestDelegate next,
        IOptions<AuthSettings> authSettings,
        IHttpClientFactory httpClientFactory,
        ILogger<JwtMiddleware> logger)
    {
        _next = next;
        _authSettings = authSettings;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = GetTokenFromHeader(context);

        if (token == null)
        {
            _logger.LogWarning("Authorization token is missing.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var userId = await ValidateTokenAndGetUserId(token);
        if (userId == null)
        {
            _logger.LogWarning("Invalid authorization token.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        context.Request.Headers.Append("UserId", userId);

        await _next(context);
    }

    private string? GetTokenFromHeader(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        return authorizationHeader?.StartsWith("Bearer ") == true
            ? authorizationHeader.Substring("Bearer ".Length).Trim()
            : null;
    }

    private async Task<string?> ValidateTokenAndGetUserId(string token)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(_authSettings.Value.AuthServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var json = JsonObject.Parse(content);
                var userId = json["userId"]?.ToString();
                _logger.LogInformation(content);
                _logger.LogInformation(userId);


                if (!string.IsNullOrEmpty(userId))
                {
                    return userId;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating token.");
        }

        return null;
    }
}