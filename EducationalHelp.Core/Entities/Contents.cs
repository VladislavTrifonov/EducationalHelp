using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class Contents : BaseEntity
    {
        public int NumberOfChapters { get; set; }

        public List<Chapter> Chapters { get; set; }
    }

    public class Chapter : BaseEntity
    {
        public string Name { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PagesCount => EndPage - StartPage;
    }
}
