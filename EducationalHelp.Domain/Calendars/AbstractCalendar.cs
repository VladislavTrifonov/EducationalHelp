using EducationalHelp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace EducationalHelp.Services.Calendars
{
    public abstract class AbstractCalendar
    {
        public const uint CalendarDaysInWeek = 7;
        public const uint CalendarDaysInMonth = 31;
        public const uint CalendarDaysInYear = 366;

        public abstract IEnumerable<CalendarEvent> GetAllPlannedEvents(Guid userId);
        public abstract IEnumerable<CalendarEvent> GetEventsBetweenDays(Guid userId, DateTime startDate, DateTime endDate);

        public virtual byte[] GetIcalRepresentation(Guid userId)
        {
            var calendar = new Ical.Net.Calendar();
            foreach (var e in this.GetAllPlannedEvents(userId))
            {
                calendar.Events.Add(new Ical.Net.CalendarComponents.CalendarEvent
                {
                    Class = "PUBLIC",
                    Summary = e.Summary,
                    Created = new CalDateTime(e.EventObject.CreatedAt),
                    Description = e.Description,
                    Start = new CalDateTime(e.DateStart),
                    End = new CalDateTime(e.DateEnd),
                    Sequence = 0,
                    Uid = e.EventObject.Id.ToString()
                });
            }

            var serializer = new CalendarSerializer(new SerializationContext());
            var serializedCalendar = serializer.SerializeToString(calendar);

            return Encoding.UTF8.GetBytes(serializedCalendar);
        }
    }
}
