using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.Module
{
    public class CreateModuleRequest
    {
        [Required]
        [MaxLength(128)]
        public string ModuleName { get; set; } = string.Empty;

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; } = string.Empty;

        // Exam Properties
        /// <summary>
        /// Maximum time allowed for student to assemble model in ms
        /// </summary>
        public long TimeLimit { get; set; }
        /// <summary>
        /// Minimum accuracy required to pass the module exam
        /// </summary>
        public long AccuracyThreshold { get; set; }
    }
}
