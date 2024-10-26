using Auth.API.Dtos;
using Auth.API.Features.Login;
using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Auth.API.Features.Validate;

public record ValidateResponse(bool Valid, string? UserId);

public class Validate : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/validate", (HttpContext httpContext, TokenValidator tokenValidator) =>
            {
                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    throw new UnathorizedServerException();
                }

                if (!tokenValidator.ValidateJwtToken(token, out ClaimsPrincipal? principal))
                {
                    throw new UnathorizedServerException();
                }

                // Optionally get the UserId from the validated token
                var userId = tokenValidator.GetUserIdFromToken(token);

                return Results.Ok(new ValidateResponse(true, userId));
            })
            .WithName("Validate")
            .Produces<ValidateResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Request validation")
            .WithDescription("Request validation");
    }
}