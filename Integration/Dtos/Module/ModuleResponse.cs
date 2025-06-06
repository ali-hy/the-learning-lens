using Integration.Dtos.LearningTask;
using Integration.Dtos.UserAccount;
using System.ComponentModel.DataAnnotations;


namespace Integration.Dtos.Module
{
    public class ModuleResponse
    {
        public long Id { get; set; }
        [MaxLength(128)]
        public string ModuleName { get; set; } = string.Empty;
        [MaxLength(2048)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public Build.Response ModelBuild { get; set; } = null!;
        [Required]
        public LearningTaskInModuleResponse ExaminationTask { get; set; } = null!;
        [Required]
        public UserAccountFlatResponse CreatedBy { get; set; } = null!;
    }
}
