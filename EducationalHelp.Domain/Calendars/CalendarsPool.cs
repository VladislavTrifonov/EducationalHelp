using EducationalHelp.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Services.Calendars
{
    /// <summary>
    /// Управляет коллекцией календарей, выполняя действия над ними, как над одним.
    /// </summary>
    public class CalendarsPool : AbstractCalendar
    {
        private readonly IEnumerable<AbstractCalendar> _calendars;

        public CalendarsPool(IEnumerable<AbstractCalendar> calendars)
        {
            if (calendars == null)
                throw new ArgumentNullException(nameof(calendars), "Calendars is null");

            if (calendars.Count() == 0)
                throw new ArgumentOutOfRangeException(nameof(calendars), "Enumerable with calendars cannot be empty.");

            _calendars = calendars; 
        }

        public override IEnumerable<CalendarEvent> GetAllPlannedEvents()
        {
            return _calendars.SelectMany(c => c.GetAllPlannedEvents());
        }

        public override IEnumerable<CalendarEvent> GetEventsBetweenDays(DateTime startDate, DateTime endDate)
        {
            return _calendars.SelectMany(c => c.GetEventsBetweenDays(startDate, endDate));
        }
    }
}
