using Domain;

namespace Infrastructure.Storage;

public class PictureStorage : IPictureStorage
{
    public async Task<string> UploadImage(string file, string fileName)
    {
        byte[] data = Convert.FromBase64String(file);

        string extension = Path.GetExtension(fileName);
        string newFileName = Guid.NewGuid() + extension;

        string path = Path.Combine(Directory.GetCurrentDirectory(), Env.Storage.Path, newFileName);

        await using var writer = File.OpenWrite(path);
        await writer.WriteAsync(data);

        return newFileName;
    }
}