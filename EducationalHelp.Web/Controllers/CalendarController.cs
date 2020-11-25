using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Calendars;
using EducationalHelp.Services.Lessons;
using EducationalHelp.Web.Models.Lessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarsManager _calendarsManager;

        public CalendarController(CalendarLessonsService calendarLessonsService)
        {
            var calendars = new List<AbstractCalendar>() { calendarLessonsService };
            _calendarsManager = new CalendarsManager(calendars);
        }

        [HttpGet("api/calendar/events")]
        public IActionResult GetEventsOnPeriod(DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < dateStart)
            {
                return BadRequest("End date can't be earlier than start date");
            }

            var events = _calendarsManager.GetEventsBetweenDays(dateStart, dateEnd);
            if (!events.Any())
            {
                return NoContent();
            }

            return new OkObjectResult(events);
        }

        [HttpGet("calendar/ics")]
        public IActionResult GetIcs()
        {
            byte[] calendarBytes = _calendarsManager.GetIcalRepresentation();
            return File(calendarBytes, "text/calendar", "calendar.ics");
        }




    }
}
