using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public Contents Contents { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
