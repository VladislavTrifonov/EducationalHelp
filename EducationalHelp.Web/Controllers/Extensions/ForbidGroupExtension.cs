using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Controllers.Extensions
{
    public static class ForbidGroupExtension
    {
        /// <summary>
        /// Возвращает ошибку "Вашей группе недоступно данное действие"
        /// </summary>
        public static IActionResult ForbidGroup(this ControllerBase controller)
        {
            return controller.Forbid();
        }
    }
}
