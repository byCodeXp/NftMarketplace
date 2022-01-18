namespace Infrastructure.Storage.Base;

public class BaseStorage
{
    protected string GenerateFileName(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        return Guid.NewGuid() + extension;
    }
}