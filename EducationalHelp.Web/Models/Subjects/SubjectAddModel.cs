using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Subjects
{
    [BindRequired]
    public class SubjectAddModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Teacher { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}
