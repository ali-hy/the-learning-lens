namespace WebApp.Models
{
    public class ReferenceImage
    {
        public long Id { get; set; }
        public string Url { get; set; } = string.Empty;

        public long ArLessonId { get; set; }
        public ArLesson ArLesson { get; set; } = null!;
    }
}
