using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetFrontendName(this ControllerBase controller)
        {
            // For example, SubjectsController => SubjeC, ErrorsController => ErrorC
            return new String(controller.ToString().Split('.').Last().Take(5).Append('C').ToArray());
        }
    }
}
