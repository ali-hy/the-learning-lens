using Integration.Dtos.Lesson;

namespace WebApp.Forms
{
    public class CreateLessonForm : CreateLessonRequestBase
    {
        public IFormFile Preview { get; set; } = null!;
        public IFormFile Prefab { get; set; } = null!;
    }
}
