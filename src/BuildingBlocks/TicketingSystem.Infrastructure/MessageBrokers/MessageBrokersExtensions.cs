using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Infrastructure.MessageBrokers.Kafka;
using TicketingSystem.Infrastructure.MessageBrokers.RabbitMQ;

namespace TicketingSystem.Infrastructure.MessageBrokers;

public static class MessageBrokersExtensions
{
    public static IServiceCollection AddKafkaSender<T>(this IServiceCollection services, KafkaOptions options)
        where T : class
    {
        services.AddSingleton<IMessageSender<T>>(new KafkaSender<T>(options.BootstrapServers, options.Topics[typeof(T).Name]));
        return services;
    }

    public static IServiceCollection AddKafkaReceiver<TConsumer, T>(this IServiceCollection services, KafkaOptions options)
        where T : class
    {
        services.AddTransient<IMessageReceiver<TConsumer, T>>(_ => new KafkaReceiver<TConsumer, T>(options.BootstrapServers,
            options.Topics[typeof(T).Name],
        options.GroupId));
        return services;
    }

    public static IServiceCollection AddRabbitMqSender<T>(this IServiceCollection services, RabbitMqOptions options)
    {
        //handle later
        return services;
    }

    public static IServiceCollection AddRabbitMqReceiver<TConsumer, T>(this IServiceCollection services, RabbitMqOptions options)
    {
        // handle later
        return services;
    }
}
