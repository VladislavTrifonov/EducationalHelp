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

        public Guid? AvatarId { get; set; }
        public virtual File Avatar { get; set; }
    }
}
