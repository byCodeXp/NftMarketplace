using Domain;
using Infrastructure.Storage.Base;

namespace Infrastructure.Storage;

public class TokenPictureStorage : BaseStorage, IStorage
{
    public async Task<string> SavePicture(string base64Picture, string filename)
    {
        string newFileName = GenerateFileName(filename);
        
        string path = Path.Combine(Directory.GetCurrentDirectory(), Env.Storage.TokenPicturePath, newFileName);
        
        await using var writer = File.OpenWrite(path);

        byte[] data = Convert.FromBase64String(base64Picture);
        await writer.WriteAsync(data);

        return newFileName;
    }
}