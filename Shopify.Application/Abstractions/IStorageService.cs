namespace Shopify.Application.Abstractions
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(byte[] fileBlob);
        Task<byte[]> GetFileAsync(string filePath);
    }
}
