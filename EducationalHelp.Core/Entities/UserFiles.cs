using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class UserFiles : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid FileId { get; set; }
        public virtual File File { get; set; }

        public UserFilesType Type { get; set; }
    }

    public enum UserFilesType
    {
        Avatar
    }
}
