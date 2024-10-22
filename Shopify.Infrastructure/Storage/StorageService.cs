using Shopify.Application.Abstractions;

namespace Shopify.Infrastructure.Storage;

// In real production example we can store the images in s3 bucket or on the server itself but in strucutred folder hirarchy no on the bin folder like now but this just for simplicity
public sealed class StorageService : IStorageService
{
    private readonly string _storageDirectory;

    public StorageService()
    {
        _storageDirectory = AppDomain.CurrentDomain.BaseDirectory;

        if (!Directory.Exists(_storageDirectory))
        {
            Directory.CreateDirectory(_storageDirectory);
        }
    }

    public async Task<byte[]> GetFileAsync(string filePath)
    {
        string fullPath = Path.Combine(_storageDirectory, filePath);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"The file '{fullPath}' was not found.");
        }

        return await File.ReadAllBytesAsync(fullPath);
    }

    public async Task<string> SaveFileAsync(byte[] fileBlob)
    {
        string fileType = GetFileType(fileBlob);

        string fileName = Guid.NewGuid().ToString() + (fileType == "image/jpeg" ? ".jpg" : ".png");
        string fullPath = Path.Combine(_storageDirectory, fileName);

        await File.WriteAllBytesAsync(fullPath, fileBlob);
        return fileName;
    }

    private string GetFileType(byte[] fileBlob)
    {
        if (fileBlob.Length >= 3 &&
            fileBlob[0] == 0xFF &&
            fileBlob[1] == 0xD8 &&
            fileBlob[2] == 0xFF)
        {
            return "image/jpeg";
        }

        if (fileBlob.Length >= 8 &&
            fileBlob[0] == 0x89 &&
            fileBlob[1] == 0x50 &&
            fileBlob[2] == 0x4E &&
            fileBlob[3] == 0x47 &&
            fileBlob[4] == 0x0D &&
            fileBlob[5] == 0x0A &&
            fileBlob[6] == 0x1A &&
            fileBlob[7] == 0x0A)
        {
            return "image/png";
        }

        throw new ArgumentException("Unsupported file type. Only JPEG and PNG");
    }
}
