namespace Integration.Dtos.Lesson
{
    public class Base
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 3;

        public string Preview { get; set; } = string.Empty;
        public Prefab.Base Prefab { get; set; } = null!; 
    }
}
