using System.Reflection;
using Microsoft.AspNetCore.Routing;

namespace TicketingSystem.Infrastructure.WebApi.MinimalApi;

public interface IEndpointHandler
{
    static abstract void MapEndpoint(IEndpointRouteBuilder builder);
}

public static class EndpointRouteBuilderExtension
{
    public static void MapEndpointHandlers(this IEndpointRouteBuilder builder, Assembly assembly)
    {
        var endpointHandlerTypes = assembly
            .GetTypes()
            .Where(x => x.GetInterfaces().Contains(typeof(IEndpointHandler)))
            .ToList();

        foreach (var item in endpointHandlerTypes)
        {
            item.InvokeMember(nameof(IEndpointHandler.MapEndpoint), BindingFlags.InvokeMethod, null, null,
                new object?[] { builder });
        }
    }
}