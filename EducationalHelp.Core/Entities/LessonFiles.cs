using System;
using EducationalHelp.Core;
using EducationalHelp.Core.Entities;

namespace EducationalHelp.Core.Entities
{
    public class LessonFiles : BaseEntity
    {
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public Guid FileId { get; set; }
        public virtual File File { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
