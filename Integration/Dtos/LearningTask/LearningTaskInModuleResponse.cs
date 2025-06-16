namespace Integration.Dtos.LearningTask
{
    public class LearningTaskInModuleResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;

        /// <summary>
        /// Maximum time allowed for student to assemble model in ms
        /// </summary>
        public int TimeLimit { get; set; } = int.MaxValue;

        /// <summary>
        /// Minimum accuracy required to pass the module exam range from 0 to 100
        /// </summary>
        public float AccuracyThreshold { get; set; } = 100;
    }
}
