namespace WebApp.Models
{
    public class ArLessonAssessment
    {
        public long UserId { get; set; }
        public UserAccount User { get; set; } = null!;

        public long ArLessonId { get; set; }
        public ArLesson ArLesson { get; set; } = null!;

        public int Score { get; set; }
    }
}
