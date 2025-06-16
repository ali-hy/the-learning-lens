using Integration.Dtos.LearningTask;
using Integration.Dtos.UserAccount;

namespace Integration.Dtos.Module
{
    public class ModuleResponse
    {
        public long Id { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Build.Response ModelBuild { get; set; } = null!;
        public LearningTaskInModuleResponse ExaminationTask { get; set; } = null!;
        public UserAccountFlatResponse CreatedBy { get; set; } = null!;
    }
}
