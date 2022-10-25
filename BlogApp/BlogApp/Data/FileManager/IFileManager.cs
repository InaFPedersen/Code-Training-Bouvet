

namespace BlogApp.Data.FileManager
{
    public interface IFileManager
    {
        FileStream Imagestream(string image);

        Task<string> SaveImage(IFormFile image);
    }
}
