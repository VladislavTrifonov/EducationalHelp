using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class User : BaseEntity
    {
        public string Pseudonym { get; set; }

        public Guid? AvatarId { get; set; }
        public virtual File Avatar { get; set; }
    }
}
