namespace TicketingSystem.Infrastructure.Storage.Local;

public class LocalFileStorageManager(LocalStorageOptions option) : IFileStorageManager
{
    public async Task CreateAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(option.Path, fileEntry.FileLocation);

        var folder = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(folder))
        {
            if (folder != null)
            {
                Directory.CreateDirectory(folder);
            }
        }

        await using var fileStream = File.Create(filePath);
        await stream.CopyToAsync(fileStream, cancellationToken);
    }

    public async Task DeleteAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            var path = Path.Combine(option.Path, fileEntry.FileLocation);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }, cancellationToken);
    }

    public Task<byte[]> ReadAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        return File.ReadAllBytesAsync(Path.Combine(option.Path, fileEntry.FileLocation), cancellationToken);
    }
}