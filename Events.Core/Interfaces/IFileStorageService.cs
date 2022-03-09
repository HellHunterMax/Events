namespace Events.Core.Interfaces
{
    public interface IFileStorageService
    {
        Task StoreFileAsync(Stream stream, string fileName);
        void DeleteFile(string fileName);
        Task<byte[]> GetFileAsBytesAsync(string fileName);
    }
}
