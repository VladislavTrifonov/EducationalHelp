using System;
using EducationalHelp.Core;
using EducationalHelp.Core.Entities;

namespace EducationalHelp.Core.Entities
{
    public class LessonFiles : BaseEntity
    {
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}
