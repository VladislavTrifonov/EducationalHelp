using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class LessonUsers : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public bool IsVisited { get; set; }
        public Mark Mark { get; set; }
    }
}
