namespace Infrastructure.Storage.Base;

public interface IStorage
{
    Task<string> SavePicture(string base64Picture, string filename);
}