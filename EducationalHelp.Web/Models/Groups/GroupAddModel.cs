using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Groups
{
    public class GroupAddModel
    {
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 1500, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
