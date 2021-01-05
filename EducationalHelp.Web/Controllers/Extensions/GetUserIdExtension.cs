using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using EducationalHelp.Services.Profile;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Controllers.Extensions
{
    public static class GetUserIdExtension
    {
        public static Guid GetUserId(this ControllerBase controller)
        {
            var authService = ServiceLocator.Current.GetInstance<JwtAuthenticationService>();
            return authService.GetUserIdFromClaims(controller.HttpContext.User.Claims);
        }
    }
}
