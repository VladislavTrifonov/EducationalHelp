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

        [JsonIgnore]
        public virtual ICollection<UserFiles> UserFiles { get; set; } = new HashSet<UserFiles>();

        public File()
        {
            LessonFiles = new HashSet<LessonFiles>();
        }
    }
}
