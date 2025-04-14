using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.LearningTask
{
    public class LearningTaskInModuleResponse
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
    }
}
