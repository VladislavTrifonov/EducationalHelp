using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EducationalHelp.Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
