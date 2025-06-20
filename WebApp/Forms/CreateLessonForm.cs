namespace WebApp.Forms
{
    public class CreateLessonForm : Integration.Dtos.Lesson.CreateRequestBase
    {
        IFormFile Preview { get; set; } = null!;
        IFormFile Prefab { get; set; } = null!;
    }
}
