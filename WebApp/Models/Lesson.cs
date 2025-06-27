using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Lesson
    {
        public long Id { get; set; }

        [MaxLength(128)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 3;

        [MaxLength(256)]
        public string Preview { get; set; } = string.Empty;

        public bool IsTest { get; set; } = false;

        public long PrefabId { get; set; }
        public Prefab Prefab { get; set; } = null!;
    }
}