namespace WebApp.Models
{
    public class Lesson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 3;
        
        public string Preview { get; set; }

        public long PrefabId { get; set; }
        public Prefab Prefab { get; set; } = null!;
    }
}
