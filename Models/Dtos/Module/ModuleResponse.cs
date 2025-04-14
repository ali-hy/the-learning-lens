using Models.Dtos.Build;
using Models.Dtos.LearningTask;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Module
{
    public class ModuleResponse
    {
        public long Id { get; set; }

        [MaxLength(128)]
        public string ModuleName { get; set; } = string.Empty;

        [MaxLength(2048)]
        public string Description { get; set; } = string.Empty;

        public BuildResponse ModelBuild { get; set; } = null!;
        public long ModelBuildId { get; set; }

        public LearningTaskInModuleResponse ExaminationTask { get; set; } = null!;
        public long ExaminationTaskId { get; set; }

        [Required]
        public UserAccount CreatedBy { get; set; } = null!;
        public long CreatedById { get; set; }
    }
}
