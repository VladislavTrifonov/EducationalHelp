using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Subjects;
using EducationalHelp.Web.Models.Subjects;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Files;
using EducationalHelp.Services.Lessons;
using File = EducationalHelp.Core.Entities.File;
using EducationalHelp.Web.Models.Lessons;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;
using EducationalHelp.Services.Profile;
using EducationalHelp.Web.Controllers.Extensions;

namespace EducationalHelp.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectsService _subjectsService;
        private readonly LessonsService _lessonsService;
        private readonly FilesService _filesService;
        private readonly UserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubjectsController(SubjectsService subjectsService,
            LessonsService lessonsService,
            FilesService filesService,
            IWebHostEnvironment webHostEnvironment,
            UserService userService)
        {
            _subjectsService = subjectsService;
            _lessonsService = lessonsService;
            _filesService = filesService;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        [HttpGet("subjects/{id}")]
        [Authorize]
        public IActionResult GetSubjectById(Guid id)
        {
            var subject = _subjectsService.GetSubject(id);
            
            if (!_userService.IsMemberOfGroup(this.GetUserId(), subject.GroupId))
            {
                return this.ForbidGroup();
            }

            return Ok(subject);
        }

        [HttpGet("subjects")]
        [Authorize]
        public IActionResult GetAllSubjects(Guid groupId)
        {
            if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
            {
                return this.ForbidGroup();
            }

            var subjects = _subjectsService.GetAllSubjects(groupId);
            return Ok(subjects);
        }

        [HttpPost("subjects")]
        [Authorize]
        public IActionResult CreateSubject(SubjectAddModel subject)
        {
            if (!_userService.IsMemberOfGroup(this.GetUserId(), subject.GroupId)) 
            {
                return this.ForbidGroup();
            }

            var subjEntity = new Subject
            {
                Name = subject.Name,
                Teacher = subject.Teacher,
                Description = subject.Description,
                UserId = this.GetUserId(),
                GroupId = subject.GroupId
            };
            
            try
            {
                _subjectsService.CreateSubject(subjEntity);
            }
            catch (ValidationException exp)
            {
                return new ObjectResult(exp.ValidationResult);
            }

            return Ok(subjEntity);
        }

        [HttpPut("subjects/{id}")]
        [Authorize]
        public IActionResult UpdateSubject([FromBody]SubjectAddModel subject, [FromRoute]Guid id)
        {
            Subject resolvedSubject;
            try
            {
                resolvedSubject = _subjectsService.GetSubject(id);
            }
            catch (ServiceException)
            {
                return NotFound();
            }

            if (!_userService.IsMemberOfGroup(this.GetUserId(), resolvedSubject.GroupId))
            {
                return this.ForbidGroup();
            }

            resolvedSubject.Name = subject.Name;
            resolvedSubject.Description = subject.Description;
            resolvedSubject.Teacher = subject.Teacher;

            _subjectsService.UpdateSubject(resolvedSubject);
            return Ok(resolvedSubject);

        }

        [HttpDelete("subjects/{id}")]
        [Authorize]
        public IActionResult DeleteSubject(Guid id)
        {
            try
            {
                var subject = _subjectsService.GetSubject(id);
                if (!_userService.IsMemberOfGroup(this.GetUserId(), subject.GroupId))
                {
                    return this.ForbidGroup();
                }
                _subjectsService.DeleteSubject(id);
            }
            catch (ServiceException)
            {
                return NotFound();
            }

            return Ok();
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

            return Ok(lessons);
        }

        [HttpGet("subjects/lessons/{lessonId}")]
        [Authorize]
        public IActionResult GetLessonById(Guid lessonId)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                var groupId = _subjectsService.GetGroupIdFromSubjectId(lesson.SubjectId);
                if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
                {
                    return this.ForbidGroup();
                }


                return Ok(lesson);
            }
            catch (ServiceException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("subjects/{id}/lessons")]
        [Authorize]
        public IActionResult AddLesson([FromRoute]Guid id, [FromBody]LessonAddModel lesson)
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
                SelfMark = Mark.None,
                Homework = lesson.Homework,
                Notes = lesson.Notes,
                SubjectId = id,
                UserId = this.GetUserId()
            };

            try
            {
                _lessonsService.CreateLesson(lessonEntity);
            }
            catch (ValidationException exp)
            {
                return new ObjectResult(exp.ValidationResult);
            }

            return Ok(lessonEntity);
        }

        [HttpPut("subjects/lessons/{lessonId}")]
        [Authorize]
        public IActionResult UpdateLesson([FromRoute]Guid lessonId, [FromBody]LessonAddModel lessonModel)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                var groupId = _subjectsService.GetGroupIdFromSubjectId(lesson.SubjectId);
                if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
                {
                    return this.ForbidGroup();
                }


                lesson.Title = lessonModel.Title;
                lesson.Label = lessonModel.Label;
                lesson.Description = lessonModel.Description;
                lesson.DateEnd = lessonModel.DateEnd;
                lesson.DateStart = lessonModel.DateStart;
                lesson.Homework = lessonModel.Homework;
                lesson.Notes = lessonModel.Notes;
                lesson.SelfMark = lessonModel.SelfMark;
                lesson.IsVisited = lessonModel.IsVisited;

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
                var groupId = _subjectsService.GetGroupIdFromSubjectId(lesson.SubjectId);
                if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
                {
                    return this.ForbidGroup();
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
        public async Task<IActionResult> LoadFiles([FromRoute]Guid lessonId, [FromForm]IFormFileCollection files)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
                var groupId = _subjectsService.GetGroupIdFromSubjectId(lesson.SubjectId);
                if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
                {
                    return this.ForbidGroup();
                }


                foreach (var formFile in files)
                {
                    var (file, fs) = _filesService.CreateNewFile(formFile.FileName, _webHostEnvironment.WebRootPath, formFile.Length);
                    await formFile.CopyToAsync(fs);
                    await fs.DisposeAsync();
                    _filesService.AttachFileToLesson(lesson.Id,file.Id);

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
                var groupId = _subjectsService.GetGroupIdFromSubjectId(lesson.SubjectId);
                if (!_userService.IsMemberOfGroup(this.GetUserId(), groupId))
                {
                    return this.ForbidGroup();
                }

                var files = lesson.LessonFiles.Select(lf => new 
                {
                    lf.File.Id,
                    lf.File.OriginalName,
                    lf.File.Length,
                    lf.File.CreatedAt,
                    lf.File.UpdatedAt,
                    lf.File.DeletedAt,
                    LinkToDownload = this.Url.Link("downloadFile", new
                    {
                        fileId = lf.File.Id
                    })
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
    }
}