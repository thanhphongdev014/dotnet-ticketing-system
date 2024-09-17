﻿using Confluent.Kafka;
using System.Text.Json;

namespace TicketingSystem.Infrastructure.MessageBrokers.Kafka;

public class KafkaReceiver<TConsumer, TData> : IMessageReceiver<TConsumer, TData>, IDisposable
    where TData : class
{
    private readonly IConsumer<Ignore, string> _consumer;

    public KafkaReceiver(string bootstrapServers, string topic, string groupId)
    {
        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe(topic);
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }

    public async Task ReceiveAsync(Func<TData, MetaData, Task> action, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);

                if (consumeResult.IsPartitionEOF)
                {
                    continue;
                }

                var message = JsonSerializer.Deserialize<Message<TData>>(consumeResult.Message.Value);
                if (message != null)
                {
                    await action(message.Data, message.MetaData);
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Consume error: {e.Error.Reason}");
            }
        }
    }
}
