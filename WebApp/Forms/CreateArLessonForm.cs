using Integration.Dtos.Lesson;

namespace WebApp.Forms
{
    public class CreateArLessonForm: CreateLessonRequestBase
    {
        public IFormFile Preview { get; set; }
        public IList<IFormFile> ReferenceImages { get; set; }
    }
}
