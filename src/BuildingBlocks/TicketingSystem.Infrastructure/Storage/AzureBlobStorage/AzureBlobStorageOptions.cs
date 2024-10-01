namespace TicketingSystem.Infrastructure.Storage.AzureBlobStorage;

public class AzureBlobStorageOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string Container { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;
}