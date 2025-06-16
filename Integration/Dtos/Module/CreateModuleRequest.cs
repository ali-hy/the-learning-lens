namespace Integration.Dtos.Module
{
    public class CreateModuleRequest
    {
        public string ModuleName { get; set; } = string.Empty;
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
