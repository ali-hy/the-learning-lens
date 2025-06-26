namespace WebApp.Services
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string directoryPath);
    }

    public class FileService : IFileService
    {
        async public Task<string> SaveFile(IFormFile file, string directoryPath)
        {
            if (file == null || file.Length <= 0)
                return string.Empty;

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(directoryPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return filePath;
        }
    }
}
