using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.User
{
    public class UserAddModel
    {
        public string Pseudonym { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
