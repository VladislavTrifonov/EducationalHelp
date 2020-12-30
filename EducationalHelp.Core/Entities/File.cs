using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EducationalHelp.Core.Entities
{
    public class File : BaseEntity
    {
        public string OriginalName { get; set; }

        [JsonIgnore]
        public string FullPath { get; set; }

        public long Length { get; set; }

        [JsonIgnore]
        public virtual ICollection<LessonFiles> LessonFiles { get; set; }

        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public File()
        {
            LessonFiles = new HashSet<LessonFiles>();
        }
    }
}
