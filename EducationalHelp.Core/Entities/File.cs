using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EducationalHelp.Core.Entities
{
    public class File : BaseEntity
    {
        public string OriginalName { get; set; }
        public string FullPath { get; set; }

        [JsonIgnore]
        public ICollection<LessonFiles> LessonFiles { get; set; }

        public File()
        {
            LessonFiles = new HashSet<LessonFiles>();
        }
    }
}
