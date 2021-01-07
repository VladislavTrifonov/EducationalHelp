using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EducationalHelp.Web.Models.Lessons
{
    [BindRequired]
    public class LessonAddModel
    {
        [Required]
        public string Title { get; set; }
        
        public string Label { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }

        public string Homework { get; set; }
        public string Notes { get; set; }

    }
}
