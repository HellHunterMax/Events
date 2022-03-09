using Events.Core.Interfaces;
using Events.Core.Options;
using Events.Core.Shared.Extentions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Events.Infrastructure.Services
{
    public class DiskFileService : IFileStorageService
    {
        private readonly ILogger<DiskFileService> _logger;
        private readonly DiskStorageSettings _storageSettings;


        public DiskFileService(IOptions<DiskStorageSettings> storageSettings, ILogger<DiskFileService> logger)
        {
            _storageSettings = storageSettings.ThrowIfNull(nameof(storageSettings)).Value;
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                fileName.ThrowIfNullOrWhiteSpace(nameof(fileName));

                var filePath = Path.Combine(_storageSettings.Location, fileName);

                if (!File.Exists(filePath)) throw new DirectoryNotFoundException(filePath);

                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                throw;
            }

        }

        public async Task<byte[]> GetFileAsBytesAsync(string fileName)
        {
            try
            {
                fileName.ThrowIfNullOrWhiteSpace(nameof(fileName));

                var filePath = Path.Combine(_storageSettings.Location, fileName);

                if (!File.Exists(filePath)) throw new DirectoryNotFoundException(filePath);

                return await File.ReadAllBytesAsync(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                throw;
            }
        }

        public async Task StoreFileAsync(Stream stream, string fileName)
        {
            try
            {
                stream.ThrowIfNull(nameof(stream));
                fileName.ThrowIfNullOrWhiteSpace(nameof(fileName));

                if (!Directory.Exists(_storageSettings.Location))
                {
                    Directory.CreateDirectory(_storageSettings.Location);
                }

                var filePath = Path.Combine(_storageSettings.Location, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                await fileStream.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                throw;
            }

        }
    }
}
