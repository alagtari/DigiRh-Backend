using BuildingBlocks.Messaging.Events;
using Conge.Application.Leaves.EventHandlers.Integration;

namespace Conge.Application;

using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });


        services.AddMessageBroker(
            configuration,
            assembly,
            config => { config.AddConsumer<AsignLeaveSupervisorsEventHsndler>(); });
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}