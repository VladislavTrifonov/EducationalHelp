using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EducationalHelp.Core.Entities
{
    public class User : BaseEntity
    {
        public string Pseudonym { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string PassHash { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserFiles> UserFiles { get; set; } = new HashSet<UserFiles>();

        [JsonIgnore]
        public virtual ICollection<GroupUsers> GroupUsers { get; set; } = new HashSet<GroupUsers>();

        [JsonIgnore]
        public virtual ICollection<LessonUsers> LessonUsers { get; set; } = new HashSet<LessonUsers>();
    }
}
