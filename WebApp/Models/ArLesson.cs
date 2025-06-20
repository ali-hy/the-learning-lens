namespace WebApp.Models
{
    public class ArLesson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 3;

        public string Preview { get; set; } = string.Empty;

        public int PassingScore { get; set; } = 50;
        public int MaxScore { get; set; } = 100;

        public IList<ReferenceImage> ReferenceImages { get; set; } = null!;
    }
}
