using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Subjects;
using EducationalHelp.Web.Models.Subjects;

namespace EducationalHelp.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private SubjectsService _subjectsService;

        public SubjectsController(SubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        [HttpGet]
        public IActionResult GetSubjectById(Guid id)
        {
            var subject = _subjectsService.GetSubject(id);
            return new ObjectResult(subject);
        }

        [HttpPost]
        public IActionResult CreateSubject(SubjectAddModel subject)
        {
          
        }


    }
}