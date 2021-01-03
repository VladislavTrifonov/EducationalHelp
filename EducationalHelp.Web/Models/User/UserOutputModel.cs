using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.User
{
    public class UserOutputModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }
        public string Pseudonym { get; set; }
        public string AvatarLink { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
