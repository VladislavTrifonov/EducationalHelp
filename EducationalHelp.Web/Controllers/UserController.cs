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
using EducationalHelp.Web.Models.User.Statistics;
using EducationalHelp.Core.Entities;
using System.IO;
using EducationalHelp.Services.Subjects;
using EducationalHelp.Services.Lessons;

namespace EducationalHelp.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthenticationService _authService;
        private readonly FilesService _filesService;
        private readonly SubjectsService _subjectsService;
        private readonly LessonsService _lessonsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(UserService userService,
            JwtAuthenticationService authService,
            FilesService filesService,
            IWebHostEnvironment webHostEnvironment,
            SubjectsService subjectsService,
            LessonsService lessonsService)
        {
            _userService = userService;
            _authService = authService;
            _filesService = filesService;
            _webHostEnvironment = webHostEnvironment;
            _subjectsService = subjectsService;
            _lessonsService = lessonsService;
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

                var outputModel = new UserOutputModel
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    DeletedAt = user.DeletedAt,
                    Login = user.Login,
                    Pseudonym = user.Pseudonym,
                };

                var avatar = _userService.GetUserAvatar(user.Id);
                if (avatar != null)
                    outputModel.AvatarLink = this.GetDownloadLink(avatar.Id);
                                
                if (userModel.Avatar != null && userModel.Avatar.Length > 0)
                {
                    var (file, fs) = _filesService.CreateNewFile(userModel.Avatar.FileName, _webHostEnvironment.WebRootPath, userModel.Avatar.Length);
                    await userModel.Avatar.CopyToAsync(fs);
                    await fs.DisposeAsync();
                    _filesService.AttachFileToUser(user.Id, file.Id, Core.Entities.UserFilesType.Avatar);

                    outputModel.AvatarLink = this.GetDownloadLink(file.Id);
                }

                _userService.UpdateUser(user);

                return Ok(outputModel);
            } 
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("api/profile/statistics")]
        [Authorize]
        public IActionResult GetUserStatistics()
        {
            var userId = _authService.GetUserIdFromClaims(HttpContext.User.Claims);
            var subjects = _subjectsService.GetAllSubjects(userId);

            var subjectStatistic = new List<UserStatisticsSubject>();

            foreach (var subject in subjects)
            {
                var statistic = new UserStatisticsSubject
                {
                    SubjectTitle = subject.Name,
                    AvgLessonsMark = _lessonsService.GetAvgLessonMarkBySubject(subject.Id),
                    LessonsCount = _lessonsService.GetNumberOfLessonsBySubject(subject.Id),
                    LessonsMissedCount = _lessonsService.GetMissedLessonsCountBySubject(subject.Id)
                };

                subjectStatistic.Add(statistic);
            }

            var outputModel = new UserStatisticsOutputModel
            {
                SubjectsCount = subjects.Count,
                AvgMarkLessonAll = _lessonsService.GetAvgLessonMarkByUser(userId),
                subjectsStatistics = subjectStatistic
            };

            return Ok(outputModel);
        }
    }
}
