using System;

namespace Integration.Dtos.ArLesson.Assessment
{
    public class Response
    {
        public bool IsExam {  get; set; }
        public DateTime StartTime { get; set; }
        public int AssessmentTime { get; set; }
        public int Score { get; set; }
        public string BitmapPath { get; set; } = string.Empty;
    }
}
