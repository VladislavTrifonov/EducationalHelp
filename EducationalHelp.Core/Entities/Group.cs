using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EducationalHelp.Core.Entities
{
    public class Group : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<GroupUsers> GroupUsers { get; set; } = new HashSet<GroupUsers>();
    }
}
