namespace TaskPRO.Application.common.interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(Stream content, string originalFileName, string subFolder, CancellationToken ct = default);
        Task<Stream> OpenReadAsync(string relativePath, CancellationToken ct = default);
        Task DeleteAsync(string relativePath, CancellationToken ct = default);
        Task<bool> FileExistsAsync(string relativePath, CancellationToken ct = default);
    }
}