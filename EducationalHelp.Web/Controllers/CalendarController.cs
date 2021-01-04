using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Calendars;
using EducationalHelp.Services.Lessons;
using EducationalHelp.Services.Profile;
using EducationalHelp.Web.Models.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarsPool _calendarsPool;
        private readonly JwtAuthenticationService _authService;

        public CalendarController(CalendarLessonsService calendarLessonsService, JwtAuthenticationService authService)
        {
            var calendars = new List<AbstractCalendar>() { calendarLessonsService };
            _calendarsPool = new CalendarsPool(calendars);
            _authService = authService;
        }

        [HttpGet("api/calendar/events")]
        [Authorize]
        public IActionResult GetEventsOnPeriod(DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < dateStart)
            {
                return BadRequest("End date can't be earlier than start date");
            }

            var events = _calendarsPool.GetEventsBetweenDays(_authService.GetUserIdFromClaims(HttpContext.User.Claims), dateStart, dateEnd);
            if (!events.Any())
            {
                return NoContent();
            }

            return new OkObjectResult(events);
        }

        [HttpGet("calendar/ics")]
        [Authorize]
        public IActionResult GetIcs()
        {
            byte[] calendarBytes = _calendarsPool.GetIcalRepresentation(_authService.GetUserIdFromClaims(HttpContext.User.Claims));
            return File(calendarBytes, "text/calendar", "calendar.ics");
        }




    }
}
