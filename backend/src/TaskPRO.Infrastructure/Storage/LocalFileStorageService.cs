using Microsoft.Extensions.Options;
using TaskPRO.Application.common.Exceptions;
using TaskPRO.Application.common.interfaces;
using TaskPRO.Application.common.Options;

namespace TaskPRO.Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _root;

        public LocalFileStorageService(IOptions<FileStorageOption> options)
        {
            _root = Path.GetFullPath(options.Value.RootPath);
            Directory.CreateDirectory(_root);
        }

        public async Task<string> SaveAsync(Stream content, string originalFileName, string subFolder, CancellationToken ct = default)
        {
            
            var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
            var storedName = $"{Guid.NewGuid():N}{ext}";
            var relative = Path.Combine(subFolder, storedName);
            var full = ResolveSafe(relative);

            Directory.CreateDirectory(Path.GetDirectoryName(full)!);

            await using var fs = new FileStream(full, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            await content.CopyToAsync(fs, ct);

            return relative.Replace('\\', '/');
        }

        public Task<Stream> OpenReadAsync(string relativePath, CancellationToken ct = default)
        {
            var full = ResolveSafe(relativePath);
            if (!File.Exists(full))
                throw new NotFoundException("The file is missing from storage.");

            Stream stream = new FileStream(full, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            return Task.FromResult(stream);
        }

        public Task DeleteAsync(string relativePath, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var full = ResolveSafe(relativePath);
            if (File.Exists(full)) File.Delete(full);
            return Task.CompletedTask;
        }

        public Task<bool> FileExistsAsync(string relativePath, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(File.Exists(ResolveSafe(relativePath)));
        }

        
        private string ResolveSafe(string relative)
        {
            var full = Path.GetFullPath(Path.Combine(_root, relative));
            if (!full.StartsWith(_root + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                throw new InvalidOperationException("Invalid file path.");
            return full;
        }
    }
}