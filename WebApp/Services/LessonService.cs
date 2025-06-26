namespace WebApp.Services
{
    public interface ILessonService
    {
        public string PrefabFilePath { get; }
        public string PreviewFilePath { get; }
        public Task<string> SavePreviewFile(IFormFile file);
        public Task<string> SavePrefabFile(IFormFile file);
    }

    public class LessonService : ILessonService
    {
        public string PrefabFilePath { get; } = Path.Combine("static", "lesson", "prefabs");
        public string PreviewFilePath { get; } = Path.Combine("static", "lesson", "previews");

        private readonly IFileService _fileService;

        public LessonService(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Saves the preview file to the server and returns the path to the saved file.
        /// </summary>
        /// <param name="file">IFormFile to be saved</param>
        /// <returns>The path to the saved file</returns>
        async public Task<string> SavePreviewFile(IFormFile file)
        {
            return await _fileService.SaveFile(file, PreviewFilePath);
        }

        /// <summary>
        /// Saves the prefab file to the server and returns the path to the saved file.
        /// </summary>
        /// <param name="file">IFormFile to be saved</param>
        /// <returns>The path to the saved file</returns>
        async public Task<string> SavePrefabFile(IFormFile file)
        {
            return await _fileService.SaveFile(file, PrefabFilePath);
        }
    }
}
