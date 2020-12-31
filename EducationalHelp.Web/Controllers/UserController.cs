using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthenticationService _authService;

        public UserController(UserService userService, JwtAuthenticationService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("api/profile/me")]
        [Authorize]
        public IActionResult GetProfileInformation()
        {
            try
            {
                var user = _userService.GetUserById(_authService.GetUserIdFromClaims(HttpContext.User.Claims));
                return new ObjectResult(user);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
