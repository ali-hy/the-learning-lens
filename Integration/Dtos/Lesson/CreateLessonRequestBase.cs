using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Dtos.Lesson
{
    public class CreateLessonRequestBase
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 3;
    }
}
