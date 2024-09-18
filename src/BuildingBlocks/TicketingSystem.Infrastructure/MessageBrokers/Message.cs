using System.Text;
using System.Text.Json;

namespace TicketingSystem.Infrastructure.MessageBrokers;

public class Message<TData>(TData data, MetaData metaData) where TData : class
{
    public MetaData MetaData { get; set; } = metaData;

    public TData Data { get; set; } = data;

    public string SerializeObject()
    {
        return JsonSerializer.Serialize(this);
    }

    public byte[] GetBytes()
    {
        return Encoding.UTF8.GetBytes(SerializeObject());
    }
}
