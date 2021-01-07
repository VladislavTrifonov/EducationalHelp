using EducationalHelp.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Lessons
{
    public class ParticipantModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public bool IsVisited { get; set; }

        [Required]
        public Mark Mark { get; set; }
    }
}
