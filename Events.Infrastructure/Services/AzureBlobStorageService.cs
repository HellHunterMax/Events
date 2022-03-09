using Azure.Storage.Blobs;
using Events.Core.Interfaces;
using Events.Core.Options;
using Events.Core.Shared.Extentions;
using Microsoft.Extensions.Options;

namespace Events.Infrastructure.Services
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AzureStorageSettings _storageSettings;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient, IOptions<AzureStorageSettings> storageSettings)
        {
            _blobServiceClient = blobServiceClient.ThrowIfNull(nameof(blobServiceClient));
            _storageSettings = storageSettings.Value;
        }

        public void DeleteFile(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_storageSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            blobClient.DeleteIfExists();
        }

        public async Task<byte[]> GetFileAsBytesAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_storageSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            if (blobClient.Exists())
            {
                using var ms = new MemoryStream();
                await blobClient.DownloadToAsync(ms);
                return ms.ToArray();
            }

            return Array.Empty<byte>();
        }

        public async Task StoreFileAsync(Stream stream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_storageSettings.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream);
        }
    }
}
