using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Client
{
    public class Module
    {
        public long Id { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Build ModelBuild { get; set; } = null!;
        public long ModelBuildId { get; set; }

        //public LearningTask ExaminationTask { get; set; } = null!;
        public long ExaminationTaskId { get; set; }

        //public UserAccount CreatedBy { get; set; } = null!;
        public long CreatedById { get; set; }
    }
}
