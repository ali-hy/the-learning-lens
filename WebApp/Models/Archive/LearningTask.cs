using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Archive
{
    public class LearningTask
    {
        public long Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2048)]
        public string Instructions { get; set; } = string.Empty;

        /// <summary>
        /// Maximum time allowed for student to assemble model in ms
        /// </summary>
        public int TimeLimit { get; set; } = int.MaxValue;

        /// <summary>
        /// Minimum accuracy required to pass the module exam range from 0 to 100
        /// </summary>
        [Range(0, 100)]
        public float AccuracyThreshold { get; set; } = 100;

        public long ModuleId { get; set; }
        public Module Module { get; set; } = null!;
    }
}
