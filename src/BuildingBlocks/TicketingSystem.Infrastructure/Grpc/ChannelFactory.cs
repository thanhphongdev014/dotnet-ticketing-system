using Grpc.Net.Client;

namespace TicketingSystem.Infrastructure.Grpc;
public static class ChannelFactory
{
    public static GrpcChannel Create(string address)
    {
        var channel = GrpcChannel.ForAddress(address);
        return channel;
    }
}

