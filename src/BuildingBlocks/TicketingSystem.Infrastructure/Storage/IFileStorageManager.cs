namespace TicketingSystem.Infrastructure.Storage;

public interface IFileStorageManager
{
    Task CreateAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);

    Task<byte[]> ReadAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default);

    Task DeleteAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default);
}

public interface IFileEntry
{
    public Guid Id { get; set; }

    public string FileName { get; set; }
    public string FileLocation { get; set; }
}