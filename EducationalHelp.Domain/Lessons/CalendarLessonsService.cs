using System;
using System.Collections.Generic;
using System.Linq;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;

namespace EducationalHelp.Services.Lessons
{
    public class CalendarLessonsService
    {
        public const uint CalendarDaysInWeek = 7;
        public const uint CalendarDaysInMonth = 31;
        public const uint CalendarDaysInYear = 366;

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
        public IEnumerable<Lesson> GetLessonsBetweenDays(DateTime startDate, DateTime endDate)
        {
            var lessons = _lessonRepository.AllData
                .Where(l => l.DateStart <= endDate && l.DateStart >= startDate);

            return lessons;
        }
        
        /// <summary>
        /// Получает ВСЕ запланированные занятия (по всем предметам, за все время)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Lesson> GetAllPlannedLessons()
        {
            return _lessonRepository.AllData;
        }
    }
}