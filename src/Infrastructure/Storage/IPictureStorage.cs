namespace Infrastructure.Storage;

public interface IPictureStorage
{
    /// <summary>
    /// Pass in parameters stream and save file to storage. Then return filename.
    /// </summary>
    /// <param name="file">File in base64</param>
    /// <param name="fileName">Original file name</param>
    /// <returns>Return filename</returns>
    Task<string> UploadImage(string file, string fileName);
}