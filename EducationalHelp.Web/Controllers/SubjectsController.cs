using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Subjects;
using EducationalHelp.Web.Models.Subjects;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Lessons;
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
        private readonly UserService _userService;

        public SubjectsController(SubjectsService subjectsService,
            LessonsService lessonsService,
            UserService userService)
        {
            _subjectsService = subjectsService;
            _lessonsService = lessonsService;
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

        
    }
}