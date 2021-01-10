using EducationalHelp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Lessons
{
    public class ShortLessonModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool CanAccess { get; set; }

        public ShortLessonModel(Lesson lesson, bool canAccess)
        {
            this.Id = lesson.Id;
            this.Title = lesson.Title;
            this.Description = lesson.Description;
            this.DateStart = lesson.DateStart;
            this.DateEnd = lesson.DateEnd;
            this.Label = lesson.Label;
            this.CanAccess = canAccess;
        }
    }
}
