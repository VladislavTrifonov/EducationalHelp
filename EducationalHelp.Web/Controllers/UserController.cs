using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Files;
using EducationalHelp.Services.Profile;
using EducationalHelp.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationalHelp.Web.Controllers.Extensions;
using EducationalHelp.Core.Entities;
using System.IO;

namespace EducationalHelp.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthenticationService _authService;
        private readonly FilesService _filesService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(UserService userService, JwtAuthenticationService authService, FilesService filesService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _authService = authService;
            _filesService = filesService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("api/profile/me")]
        [Authorize]
        public IActionResult GetProfileInformation()
        {
            try
            {
                var user = _userService.GetUserById(_authService.GetUserIdFromClaims(HttpContext.User.Claims));
                var avatar = _userService.GetUserAvatar(user.Id);

                var outputModel = new UserOutputModel
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    DeletedAt = user.DeletedAt,
                    Login = user.Login,
                    Pseudonym = user.Pseudonym,
                };

                if (avatar != null)
                {
                    outputModel.AvatarLink = this.GetDownloadLink(avatar.Id);
                }

                return new ObjectResult(outputModel);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("api/profile/me")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileInformation([FromForm] UserAddModel userModel)
        {
            try
            {
                var user = _userService.GetUserById(_authService.GetUserIdFromClaims(HttpContext.User.Claims));

                user.Pseudonym = userModel.Pseudonym;
                Core.Entities.File file = null;
                FileStream fs;
                if (userModel.Avatar != null && userModel.Avatar.Length > 0)
                {
                    (file, fs) = _filesService.CreateNewFile(userModel.Avatar.FileName, _webHostEnvironment.WebRootPath, userModel.Avatar.Length);
                    await userModel.Avatar.CopyToAsync(fs);
                    await fs.DisposeAsync();
                    _filesService.AttachFileToUser(user.Id, file.Id, Core.Entities.UserFilesType.Avatar);
                }

                var outputModel = new UserOutputModel
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    DeletedAt = user.DeletedAt,
                    Login = user.Login,
                    Pseudonym = user.Pseudonym,
                    AvatarLink = this.GetDownloadLink(file.Id)
                };

                _userService.UpdateUser(user);

                return Ok(outputModel);
            } 
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
