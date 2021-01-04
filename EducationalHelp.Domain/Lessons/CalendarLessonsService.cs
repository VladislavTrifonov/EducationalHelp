using System;
using System.Collections.Generic;
using System.Linq;
using EducationalHelp.Core;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Calendars;

namespace EducationalHelp.Services.Lessons
{
    public class CalendarLessonsService : AbstractCalendar
    {
        private readonly LessonsService _lessonsService;
        private readonly IRepository<Lesson> _lessonRepository;


        public CalendarLessonsService(LessonsService lessonsService, IRepository<Lesson> lessonRepository)
        {
            _lessonsService = lessonsService;
            _lessonRepository = lessonRepository;
        }

        /// <summary>
        /// Получает занятия, запланированные в промежутке [startDate, endDate]
        /// </summary>
        /// <param name="startDate">Стартовая дата</param>
        /// <param name="endDate">Конечная дата</param>
        /// <returns></returns>
        public override IEnumerable<CalendarEvent> GetEventsBetweenDays(Guid userId, DateTime startDate, DateTime endDate)
        {
            var lessons = _lessonsService.GetLessonsByUser(userId)
                .Where(l => l.DateStart <= endDate && l.DateStart >= startDate)
                .Select(InitalizeCalendarEventFromLesson);

            return lessons;
        }
        
        /// <summary>
        /// Получает ВСЕ запланированные занятия (по всем предметам, за все время)
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<CalendarEvent> GetAllPlannedEvents(Guid userId)
        {
            return _lessonsService.GetLessonsByUser(userId).Select(InitalizeCalendarEventFromLesson);
        }

        private CalendarEvent InitalizeCalendarEventFromLesson(Lesson lesson)
        {
            return new CalendarEvent
            {
                DateStart = lesson.DateStart,
                DateEnd = lesson.DateEnd,
                Description = lesson.Description,
                EventObject = lesson,
                Summary = $"Занятие, тема: {lesson.Title}",
                Type = "Lesson"
            };
        }
    }
}