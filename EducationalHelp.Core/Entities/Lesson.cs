using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EducationalHelp.Core.Entities
{
    public class Lesson : BaseEntity
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public string Title { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public bool IsVisited { get; set; }
        public Mark SelfMark { get; set; }

        public string Homework { get; set; }

        public string Notes { get; set; }

        public Guid SubjectId { get; set; }

        [JsonIgnore]
        public virtual Subject Subject { get; set; }

        [JsonIgnore]
        public virtual ICollection<LessonFiles> LessonFiles { get; set; }

        public Lesson()
        {
            LessonFiles = new HashSet<LessonFiles>();
        }
    }
}
