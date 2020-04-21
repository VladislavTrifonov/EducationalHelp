using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Subjects
{
    [BindRequired]
    public class SubjectAddModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
    }
}
