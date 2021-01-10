using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Files;
using EducationalHelp.Services.Groups;
using EducationalHelp.Services.Lessons;
using EducationalHelp.Services.Profile;
using EducationalHelp.Services.Subjects;
using EducationalHelp.Web.Controllers.Extensions;
using EducationalHelp.Web.Models.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly SubjectsService _subjectsService;
        private readonly LessonsService _lessonsService;
        private readonly FilesService _filesService;
        private readonly UserService _userService;
        private readonly GroupService _groupsService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public LessonsController(SubjectsService subjectsService,
            LessonsService lessonsService,
            FilesService filesService,
            IWebHostEnvironment webHostEnvironment,
            UserService userService,
            GroupService groupsService)
        {
            _subjectsService = subjectsService;
            _lessonsService = lessonsService;
            _filesService = filesService;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _groupsService = groupsService;
        }


        [HttpGet("subjects/{id}/lessons")]
        [Authorize]
        public IActionResult GetLessons(Guid id)
        {
            var groupId = _subjectsService.GetGroupIdFromSubjectId(id);
            if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
            {
                return this.ForbidGroup();
            }

            var lessons = _lessonsService.GetLessonsBySubjectId(id);
            if (!lessons.Any())
                return NotFound();

            var outputLessons = lessons.Select(l => new ShortLessonModel(l, _lessonsService.CanUserAccessToLesson(this.GetUserId(), l.Id)));

            return Ok(outputLessons);
        }

        [HttpGet("subjects/lessons/{lessonId}")]
        [Authorize]
        public IActionResult GetLessonById(Guid lessonId)
        {
            try
            {
                if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
                {
                    return Forbid();
                }

                var lesson = _lessonsService.GetLessonById(lessonId);
                return Ok(lesson);
            }
            catch (ServiceException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("subjects/{id}/lessons")]
        [Authorize]
        public IActionResult AddLesson([FromRoute] Guid id, [FromBody] LessonAddModel lesson)
        {
            var groupId = _subjectsService.GetGroupIdFromSubjectId(id);
            if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
            {
                return this.ForbidGroup();
            }

            var lessonEntity = new Lesson
            {
                Title = lesson.Title,
                Label = lesson.Label,
                Description = lesson.Description,
                DateStart = lesson.DateStart,
                DateEnd = lesson.DateEnd,
                Homework = lesson.Homework,
                Notes = lesson.Notes,
                SubjectId = id
            };

            try
            {
                _lessonsService.CreateLesson(lessonEntity, this.GetUserId());
            }
            catch (ValidationException exp)
            {
                return new ObjectResult(exp.ValidationResult);
            }

            return Ok(lessonEntity);
        }

        [HttpPut("subjects/lessons/{lessonId}")]
        [Authorize]
        public IActionResult UpdateLesson([FromRoute] Guid lessonId, [FromBody] LessonAddModel lessonModel)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
                {
                    return Forbid();
                }


                lesson.Title = lessonModel.Title;
                lesson.Label = lessonModel.Label;
                lesson.Description = lessonModel.Description;
                lesson.DateEnd = lessonModel.DateEnd;
                lesson.DateStart = lessonModel.DateStart;
                lesson.Homework = lessonModel.Homework;
                lesson.Notes = lessonModel.Notes;

                _lessonsService.UpdateLesson(lesson);
                return Ok(lesson);
            }
            catch (ServiceException e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpDelete("subjects/lessons/{lessonId}")]
        [Authorize]
        public IActionResult DeleteLesson(Guid lessonId)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
                {
                    return Forbid();
                }

                _lessonsService.DeleteLesson(lessonId);
                return Ok();
            }
            catch (ServiceException)
            {
                return NotFound($"Lesson with id {lessonId} wasn't found");
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost("subjects/lessons/{lessonId}/files")]
        [Authorize]
        public async Task<IActionResult> LoadFiles([FromRoute] Guid lessonId, [FromForm] IFormFileCollection files)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
                {
                    return Forbid();
                }


                foreach (var formFile in files)
                {
                    var (file, fs) = _filesService.CreateNewFile(formFile.FileName, _webHostEnvironment.WebRootPath, formFile.Length);
                    await formFile.CopyToAsync(fs);
                    await fs.DisposeAsync();
                    _filesService.AttachFileToLesson(new LessonFiles()
                    {
                        LessonId = lesson.Id,
                        FileId = file.Id,
                        UserId = this.GetUserId()
                    });

                }

                return Ok();
            }
            catch (ServiceException)
            {
                return NotFound($"Lesson with id {lessonId} wasn't found");
            }
        }

        [HttpGet("subjects/lessons/{lessonId}/files")]
        [Authorize]
        public IActionResult GetAllFilesByLessonId(Guid lessonId)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
                {
                    return Forbid();
                }

                var files = lesson.LessonFiles.Where(lf => lf.UserId == this.GetUserId()).Select(lf => new
                {
                    lf.File.Id,
                    lf.File.OriginalName,
                    lf.File.Length,
                    lf.File.CreatedAt,
                    lf.File.UpdatedAt,
                    lf.File.DeletedAt,
                    LinkToDownload = this.GetDownloadLink(lf.File.Id)
                }).ToArray();


                if (files.Length == 0)
                {
                    return NoContent();
                }

                return new OkObjectResult(files);
            }
            catch (ServiceException)
            {
                return NotFound(lessonId);
            }
        }

        [HttpGet("subjects/lessons/{lessonId}/participants")]
        [Authorize]
        public IActionResult GetAllParticipants(Guid lessonId)
        {
            if (!_lessonsService.IsExist(lessonId))
            {
                return NotFound(lessonId);
            }

            if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
            {
                return Forbid();
            }

            var participants = _lessonsService.GetLessonParticipants(lessonId).Select(l => new ParticipantModel()
            {
                UserId = l.UserId,
                IsVisited = l.IsVisited,
                Mark = l.Mark
            });

            return new ObjectResult(participants);
        }

        [HttpPost("subjects/lessons/{lessonId}/participants")]
        [Authorize]
        public IActionResult AddParticipant(Guid lessonId, [FromBody] ParticipantAddModel participant)
        {
            if (!_lessonsService.IsExist(lessonId))
            {
                return NotFound(lessonId);
            }

            if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
            {
                return Forbid();
            }

            if (_lessonsService.IsUserParticipate(lessonId, participant.UserId))
            {
                return BadRequest($"User with id {participant.UserId} already participant of lesson {lessonId}");
            }

            _lessonsService.AddParticipant(lessonId, participant.UserId);

            return Ok();
        }

        [HttpDelete("subjects/lessons/{lessonId}/participants/{userId}")]
        [Authorize]
        public IActionResult RemoveParticipant(Guid lessonId, Guid userId)
        {
            if (!_lessonsService.IsExist(lessonId))
            {
                return NotFound(lessonId);
            }

            if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
            {
                return Forbid();
            }

            if (!_lessonsService.IsUserParticipate(lessonId, userId))
            {
                return BadRequest(userId);
            }

            _lessonsService.RemoveParticipant(lessonId, userId);

            return Ok();
        }

        [HttpPut("subjects/lessons/{lessonId}/participants")]
        [Authorize]
        public IActionResult UpdateParticipant([FromRoute] Guid lessonId, [FromBody] ParticipantModel participant)
        {
            if (!_lessonsService.IsExist(lessonId))
            {
                return NotFound(lessonId);
            }

            if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
            {
                return Forbid();
            }

            if (!_lessonsService.IsUserParticipate(lessonId, participant.UserId))
            {
                return BadRequest(participant.UserId);
            }

            var lessonUser = _lessonsService.GetLessonParticipant(lessonId, participant.UserId);

            lessonUser.IsVisited = participant.IsVisited;
            lessonUser.Mark = participant.Mark;

            _lessonsService.UpdateParticipant(lessonUser);

            return Ok();
        }

        [HttpGet("subjects/lessons/{lessonId}/participants/possible")]
        [Authorize]
        public IActionResult GetPossibleParticipants([FromRoute] Guid lessonId)
        {
            if (!_lessonsService.CanUserAccessToLesson(this.GetUserId(), lessonId))
            {
                return Forbid();
            }

            var users = _lessonsService.GetPossibleParticipants(lessonId);

            return Ok(users);
        }
    }
}
