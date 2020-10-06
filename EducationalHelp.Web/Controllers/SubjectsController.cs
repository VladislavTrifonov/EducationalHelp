using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Subjects;
using EducationalHelp.Web.Models.Subjects;
using EducationalHelp.Services.Exceptions;

namespace EducationalHelp.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectsService _subjectsService;

        public SubjectsController(SubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        [HttpGet("subjects/{id}")]
        public IActionResult GetSubjectById(Guid id)
        {
            var subject = _subjectsService.GetSubject(id);
            return new ObjectResult(subject);
        }

        [HttpGet("subjects")]
        public IActionResult GetAllSubjects()
        {
            var subjects = _subjectsService.GetAllSubjects();
            return new ObjectResult(subjects);
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


    }
}