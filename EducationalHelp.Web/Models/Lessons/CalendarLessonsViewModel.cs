using System;

namespace EducationalHelp.Web.Models.Lessons
{
    public class CalendarLessonsViewModel
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type => "Lesson";
    }
}