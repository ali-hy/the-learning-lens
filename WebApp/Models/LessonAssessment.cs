namespace WebApp.Models
{
    public class LessonAssessment
    {
        public long UserId { get; set; }
        public UserAccount User { get; set; } = null!;

        public long VrLessonId { get; set; }
        public Lesson VrLesson { get; set; } = null!;

        public int Score { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}
