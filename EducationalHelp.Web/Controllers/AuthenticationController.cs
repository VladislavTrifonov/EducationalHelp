using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Core.Entities;
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
        private readonly UserService _userService;

        public AuthenticationController(JwtAuthenticationService authenticationService, UserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("api/auth/token")]
        public IActionResult Authorize([FromBody] LoginViewModel loginModel)
        {
            try
            {
                var credentials = new UserCredentials(loginModel.Login, loginModel.Password);
                var token = _authenticationService.CreateAccessToken(credentials);

                return new ObjectResult(new AccessTokenOutputModel(token, credentials));
            }
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("api/auth/register")]
        public IActionResult Register([FromBody] RegisterViewModel registerModel)
        {
            try
            {
                var credentials = new UserCredentials(registerModel.Login, registerModel.Password);
                var user = new User() 
                { 
                    Pseudonym = registerModel.Pseudonym
                };
                _authenticationService.UpdateCredentials(user, credentials);
                _userService.AddUser(user);

                var token = _authenticationService.CreateAccessToken(credentials);

                return Ok(new AccessTokenOutputModel(token, credentials));
            }
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
        }
       
    }
}
