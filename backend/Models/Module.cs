using System.ComponentModel.DataAnnotations;

namespace the_learning_lens.Models
{
    public class Module
    {
        public long Id { get; set; }

        [MaxLength(128)]
        public string ModuleName { get; set; } = string.Empty;

        [MaxLength(2048)]
        public string Description { get; set; } = string.Empty;

        public long ModelBuildId { get; set; }
        public Build ModelBuild { get; set; } = null!;

        public long ExaminationTaskId { get; set; }
        public LearningTask ExaminationTask { get; set; } = null!;

        [Required]
        public UserAccount CreatedBy { get; set; } = null!;

    }
}
