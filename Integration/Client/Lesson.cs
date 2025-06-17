namespace Integration.Client
{
    [System.Serializable]
    public class Lesson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 3;
    }
}
