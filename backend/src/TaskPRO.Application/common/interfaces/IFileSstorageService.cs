namespace TaskPRO.Application.common.interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(string content, string fileName,string Subfolder, CancellationToken ct = default);
        Task<Stream> GetFileAsync(string filePath, CancellationToken ct = default);
        Task DeleteFileAsync(string filePath, CancellationToken ct = default);
        Task<bool> FileExistsAsync(string filePath, CancellationToken ct = default);
    }
}