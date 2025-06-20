using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Archive
{
    public class Module
    {
        public long Id { get; set; }

        [MaxLength(128)]
        public string ModuleName { get; set; } = string.Empty;

        [MaxLength(2048)]
        public string Description { get; set; } = string.Empty;

        public Build ModelBuild { get; set; } = null!;
        public long ModelBuildId { get; set; }

        public LearningTask ExaminationTask { get; set; } = null!;
        public long ExaminationTaskId { get; set; }

        [Required]
        public UserAccount CreatedBy { get; set; } = null!;
        public long CreatedById { get; set; }
    }
}
