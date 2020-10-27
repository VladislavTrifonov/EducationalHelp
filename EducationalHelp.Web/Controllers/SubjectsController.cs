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

namespace EducationalHelp.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectsService _subjectsService;
        private readonly LessonsService _lessonsService;
        private readonly FilesService _filesService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubjectsController(SubjectsService subjectsService,
            LessonsService lessonsService,
            FilesService filesService,
            IWebHostEnvironment webHostEnvironment)
        {
            _subjectsService = subjectsService;
            _lessonsService = lessonsService;
            _filesService = filesService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("subjects/{id}")]
        public IActionResult GetSubjectById(Guid id)
        {
            var subject = _subjectsService.GetSubject(id);
            return Ok(subject);
        }

        [HttpGet("subjects")]
        public IActionResult GetAllSubjects()
        {
            var subjects = _subjectsService.GetAllSubjects();
            return Ok(subjects);
        }

        [HttpPost("subjects")]
        public IActionResult CreateSubject(SubjectAddModel subject)
        {
            var subjEntity = new Subject
            {
                Name = subject.Name,
                Teacher = subject.Teacher,
                Description = subject.Description
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
            resolvedSubject.Name = subject.Name;
            resolvedSubject.Description = subject.Description;
            resolvedSubject.Teacher = subject.Teacher;

            _subjectsService.UpdateSubject(resolvedSubject);
            return Ok(resolvedSubject);

        }

        [HttpDelete("subjects/{id}")]
        public IActionResult DeleteSubject(Guid id)
        {
            try
            {
                _subjectsService.DeleteSubject(id);
            }
            catch (ServiceException)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("subjects/{id}/lessons")]
        public IActionResult GetLessons(Guid id)
        {
            var lessons = _lessonsService.GetLessonsBySubjectId(id);
            if (!lessons.Any())
                return NotFound();

            return Ok(lessons);
        }

        [HttpGet("subjects/{subjectId}/lessons/{lessonId}")]
        public IActionResult GetLessonById(Guid subjectId, Guid lessonId)
        {
            try
            {
                var subject = _subjectsService.GetSubject(subjectId);
                var lesson = subject.Lessons.FirstOrDefault(l => l.Id == lessonId);
                if (lesson == null)
                {
                    return NotFound($"Lesson with id {lessonId} of subject with id {subjectId} was not found");
                }

                return Ok(lesson);
            }
            catch (ServiceException)
            {
                return NotFound($"Subject with id {subjectId} was not found");
            }
        }

        [HttpPost("subjects/{id}/lessons")]
        public IActionResult AddLesson([FromRoute]Guid id, [FromBody]LessonAddModel lesson)
        {
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
                SubjectId = id
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

        [HttpPut("subjects/{subjectId}/lessons/{lessonId}")]
        public IActionResult UpdateLesson([FromRoute]Guid subjectId, [FromRoute]Guid lessonId, [FromBody]LessonAddModel lessonModel)
        {
            try
            {
                var subject = _subjectsService.GetSubject(subjectId);
                var lesson = subject.Lessons.FirstOrDefault(l => l.Id == lessonId);
                if (lesson == null)
                {
                    return NotFound($"Lesson with id {lessonId} was not found");
                }

                lesson.Title = lessonModel.Title;
                lesson.Label = lessonModel.Label;
                lesson.Description = lessonModel.Description;
                lesson.DateEnd = lessonModel.DateEnd;
                lesson.DateStart = lessonModel.DateStart;
                lesson.Homework = lessonModel.Homework;
                lesson.Notes = lessonModel.Notes;
                lesson.SubjectId = subjectId;
                lesson.SelfMark = lessonModel.SelfMark;
                lesson.IsVisited = lessonModel.IsVisited;

                _lessonsService.UpdateLesson(lesson);
                return Ok(lesson);
            }
            catch (ServiceException)
            {
                return NotFound($"Subject with id {subjectId} wasn't found");
            }

        }

        [HttpDelete("subjects/{_}/lessons/{lessonId}")]
        public IActionResult DeleteLesson(Guid lessonId)
        {
            try
            {
                _lessonsService.DeleteLesson(lessonId);
                return Ok();
            }
            catch (ServiceException)
            {
                return NotFound($"Lesson with id {lessonId} wasn't found");
            }
        }

        [HttpPost("subjects/{_}/lessons/{lessonId}/files")]
        public async Task<IActionResult> LoadFiles([FromRoute]Guid lessonId, [FromForm]IFormFileCollection files)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);

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

        [HttpGet("subjects/{_}/lessons/{lessonId}/files")]
        public IActionResult GetAllFilesByLessonId(Guid lessonId)
        {
            try
            {
                var lesson = _lessonsService.GetLessonById(lessonId);
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