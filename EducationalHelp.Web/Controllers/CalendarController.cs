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
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarLessonsService _calendarLessonsService;

        public CalendarController(CalendarLessonsService calendarLessonsService)
        {
            _calendarLessonsService = calendarLessonsService;
        }

        [HttpGet("api/calendar/events/{year}")]
        public IActionResult GetEventsOnYear([FromRoute]int year)
        {
            if (year < 1 || year > 9999)
            {
                return BadRequest(year);
            }

            var lessonEvents =
                _calendarLessonsService
                    .GetLessonsBetweenDays(new DateTime(year, 1, 1), CalendarLessonsService.CalendarDaysInYear)
                    .Select(l => new CalendarLessonsViewModel()
                    {
                        Id = l.Id,
                        Name = l.Title,
                        DateStart = l.DateStart,
                        DateEnd = l.DateEnd,
                        Label = l.Label

                    });

            if (!lessonEvents.Any())
            {
                return NotFound(year);
            }

            return new OkObjectResult(lessonEvents);
        }

    }
}
