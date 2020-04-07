using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }

        public File Annotation { get; set; }
        public Guid AnnotationId { get; set; }

        public File Program { get; set; }
        public Guid ProgramId { get; set; }

        public File ValuationTools { get; set; }
        public Guid ValuationToolsId { get; set; }

        public List<Book> Books { get; set; }
    }
}
