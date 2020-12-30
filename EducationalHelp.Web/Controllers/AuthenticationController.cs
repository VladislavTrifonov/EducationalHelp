using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Profile;
using EducationalHelp.Web.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtAuthenticationService _authenticationService;

        public AuthenticationController(JwtAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("api/auth/token")]
        public IActionResult Authorize([FromBody] LoginViewModel loginModel)
        {
            try
            {
                var credentials = new UserCredentials(loginModel.Login, loginModel.Password);
                var token = _authenticationService.CreateAccessToken(credentials);

                return new ObjectResult(new
                {
                    access_token = token,
                    user = loginModel.Login
                });
            }
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("api/auth/register")]
        public IActionResult Register()
        {
            return NoContent();
        }

       
    }
}
