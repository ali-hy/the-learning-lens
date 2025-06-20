namespace WebApp.Forms
{
    public class CreateArLessonForm: Integration.Dtos.Lesson.CreateRequestBase
    {
        public IFormFile Preview { get; set; }
        public IList<IFormFile> ReferenceImages { get; set; }
    }
}
