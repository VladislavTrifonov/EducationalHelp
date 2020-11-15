using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Lessons;
using EducationalHelp.Web.Models.Lessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarLessonsService _calendarLessonsService;

        public CalendarController(CalendarLessonsService calendarLessonsService)
        {
            _calendarLessonsService = calendarLessonsService;
        }

        [HttpGet("calendar/events")]
        public IActionResult GetEventsOnPeriod(DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < dateStart)
            {
                return BadRequest("End date can't be earlier than start date");
            }

            var lessonEvents =
                _calendarLessonsService
                    .GetLessonsBetweenDays(dateStart, dateEnd)
                    .Select(l => new CalendarLessonsViewModel()
                    {
                        Id = l.Id,
                        Name = l.Title,
                        DateStart = l.DateStart,
                        DateEnd = l.DateEnd,
                        Label = l.Label,
                        SubjectId = l.SubjectId

                    });

            if (!lessonEvents.Any())
            {
                return NotFound();
            }

            return new OkObjectResult(lessonEvents);
        }

    }
}
