using System.Text;
using System.Text.Json;

namespace TicketingSystem.Infrastructure.MessageBrokers;
public class Message<TData>(TData data) where TData : class
{
    public MetaData MetaData { get; set; } = new();

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
