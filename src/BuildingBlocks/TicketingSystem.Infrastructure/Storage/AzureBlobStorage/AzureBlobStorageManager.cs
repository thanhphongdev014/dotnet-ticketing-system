using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace TicketingSystem.Infrastructure.Storage.AzureBlobStorage;

public class AzureBlobStorageManager : IFileStorageManager
{
    private readonly AzureBlobStorageOptions _option;
    private readonly BlobContainerClient _container;

    public AzureBlobStorageManager(AzureBlobStorageOptions option)
    {
        _option = option;
        _container = new BlobContainerClient(_option.ConnectionString, _option.Container);
    }

    private string GetBlobName(IFileEntry fileEntry)
    {
        return _option.Path + fileEntry.FileLocation;
    }

    public async Task CreateAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default)
    {
        await _container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        BlobClient blob = _container.GetBlobClient(GetBlobName(fileEntry));
        await blob.UploadAsync(stream, overwrite: true, cancellationToken);
    }

    public async Task DeleteAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        BlobClient blob = _container.GetBlobClient(GetBlobName(fileEntry));
        await blob.DeleteAsync(cancellationToken: cancellationToken);
    }

    public async Task<byte[]> ReadAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        BlobClient blob = _container.GetBlobClient(GetBlobName(fileEntry));
        using var stream = new MemoryStream();
        await blob.DownloadToAsync(stream, cancellationToken);
        return stream.ToArray();
    }

    public async Task ArchiveAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        BlobClient blob = _container.GetBlobClient(GetBlobName(fileEntry));
        await blob.SetAccessTierAsync(AccessTier.Cool, cancellationToken: cancellationToken);
    }

    public async Task UnArchiveAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        BlobClient blob = _container.GetBlobClient(GetBlobName(fileEntry));
        await blob.SetAccessTierAsync(AccessTier.Hot, cancellationToken: cancellationToken);
    }
}