using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public byte Rating { get; set; }
        public string Cover { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
